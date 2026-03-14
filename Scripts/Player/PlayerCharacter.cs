using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public interface IInteractable
{
    void StartClick();
    void StopClick();
}
public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance { get; private set; }


    [SerializeField] private float speed;
    [SerializeField] private float range;


    [Header("References")]
    [SerializeField] private SpriteRenderer[] joshDirections;
    [SerializeField] private GameObject dynomitePrefab;
    [SerializeField] private GameObject healthContainer;
    [SerializeField] private RectMask2D healthMask;
    [SerializeField] private Image healthColor;
    [SerializeField] private Gradient healthGradient;

    [Header("Inputs")]
    [SerializeField] private InputActionReference horizontal;
    [SerializeField] private InputActionReference vertical;
    [SerializeField] private InputActionReference pickupButton;
    [SerializeField] private InputActionReference shootButton;
    [SerializeField] private InputActionReference dynomiteButton;

    [SerializeField] private AudioClip pickupSound;

    private Rigidbody2D rb2D;

    public ItemPickup currentItem = null;
    private bool canPickup = false;
    private bool canShoot = false;
    private bool canDynomite = true;
    private bool freeze = false;
    public float health { get; private set; } = 100;
    public bool hasMoved { get; private set; } = false;
    public bool hasUsedItem { get; private set; } = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentItem = null;
    }
    private void Update()
    {
        healthMask.padding = new Vector4(0, 0, (1 - health / 100f) * healthMask.GetComponent<RectTransform>().rect.width, 0);
        healthColor.color = healthGradient.Evaluate(health / 100f);
    }

    private void FixedUpdate()
    {
        if (freeze)
            return;
        Vector2 velocity = Vector2.zero;

        velocity.y = vertical.action.ReadValue<float>();
        velocity.x = horizontal.action.ReadValue<float>();

        velocity.Normalize();

        velocity *= speed;

        rb2D.AddForce(velocity, ForceMode2D.Impulse);



        if (velocity != new Vector2(0, 0))
        {
            hasMoved = true;
            for (int i = 0; i < joshDirections.Length; i++)
            {
                joshDirections[i].enabled = false;
            }
        }
        if (velocity.y > 0)
            joshDirections[0].enabled = true;
        if (velocity.x > 0)
            joshDirections[1].enabled = true;
        if (velocity.y < 0)
            joshDirections[2].enabled = true;
        if (velocity.x < 0)
            joshDirections[3].enabled = true;


        if (pickupButton.action.ReadValue<float>() == 1)
        {
            if (canPickup)
            {
                if (currentItem == null)
                {
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0);
                    foreach (RaycastHit2D hit in hits)
                    {
                        Debug.Log(hit.transform.name);
                        if (hit.transform.tag == "gun")
                        {
                            currentItem = hit.transform.GetComponent<ItemPickup>();
                            currentItem.Pickup();
                            AudioManager.Instance.PlaySound(pickupSound,1,1);
                            break;
                        }
                    }

                }
                else
                {
                    currentItem.GetComponent<IInteractable>().StopClick();
                    currentItem.Drop();
                    currentItem = null;
                }
                canPickup = false;

            }
        }
        else
        {
            canPickup = true;
        }

        if (shootButton.action.ReadValue<float>() == 1)
        {
            if (canShoot && currentItem != null && currentItem.GetComponent<IInteractable>() != null)
            {
                hasUsedItem = true;
                currentItem.GetComponent<IInteractable>().StartClick();
            }
            canShoot = false;
        }
        else if (!canShoot)
        {
            if (currentItem != null)
                currentItem.GetComponent<IInteractable>().StopClick();
            canShoot = true;
        }

        if (dynomiteButton.action.ReadValue<float>() == 1)
        {
            if (canDynomite && LevelGenerator.Instance.dynomite > 0)
            {
                Debug.Log(currentItem);
                Drop();
                LevelGenerator.Instance.UseDynomite();
                GameObject x = Instantiate(dynomitePrefab);
                currentItem = x.GetComponent<ItemPickup>();
                currentItem.Pickup();
                canDynomite = false;
            }
        }
        else
        {
            canDynomite = true;
        }

        if (health <= 0)
        {
            DataHolder.Instance.SaveData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void SetPosition(Vector2 position)
    {
        foreach (Door door in Door.Instances)
        {
            door.StopDooring();
        }
        transform.position = position;
    }
    public void Drop()
    {
        if (currentItem == null)
            return;
        currentItem.GetComponent<IInteractable>().StopClick();
        currentItem.Drop();
        currentItem = null;
    }
    public void Damage(int amount)
    {
        health -= amount;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "hurtPlayer")
            health -= other.GetComponent<EnemyDamage>().GetDamage();
    }
    public void Freeze()
    {
        freeze = true;
        rb2D.isKinematic = true;
        rb2D.velocity = Vector3.zero;
    }
    public void Unfreeze()
    {
        freeze = false;
        rb2D.isKinematic = false;
    }
    public void AddHealth(float amt)
    {
        health += amt;
        if (health > 100)
            health = 100;
    }
    public void SetHealth(float amt)
    {
        health = amt;
    }
    public void SetItem(ItemPickup item)
    {
        currentItem = item;
    }
    public void ShowHealthBar()
    {
        healthContainer.SetActive(true);
    }
}
