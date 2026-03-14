using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private Transform visual;
    [SerializeField] private float stopDistance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private SpriteRenderer flashHurt;
    [SerializeField] private Vector2 hurtArea;
    [SerializeField] private RectMask2D healthMask;
    [SerializeField] private Image healthColor;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private int coinDrop;
    [SerializeField] private AudioClip hurtSound;
    private float originalHealth;
    private Rigidbody2D rb2D;
    private float time;
    private float hitCooldown = .3f;
    private float flashTime = 0;
    private bool overidePosition = false;
    private Vector3 target;
    private bool doStop = false;

    private void Start()
    {
        originalHealth = health;
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        healthMask.padding = new Vector4(0, 0, (1 - health / originalHealth) * 124, 0);
        healthColor.color = healthGradient.Evaluate(health / originalHealth);

        if (!overidePosition)
        {
            target = PlayerCharacter.Instance.transform.position;
        }

        if (Vector2.Distance(transform.position, target) > stopDistance)
        {
            Vector2 velocity = target - transform.position;
            velocity.Normalize();
            velocity *= speed;
            velocity *= Time.deltaTime;

            rb2D.AddForce(velocity, ForceMode2D.Impulse);

            visual.localPosition = new Vector2(0, Mathf.Abs(Mathf.Cos(time * 10)) / 4);
            visual.localScale = new Vector2(1 + Mathf.Abs(Mathf.Cos(time * 10)) / 8, 1);
            time += Time.deltaTime / 3;
            doStop = true;
        }
        else if (doStop)
        {
            rb2D.velocity = new Vector2(0, 0);
            doStop = false;
        }


        if (hitCooldown < 0)
        {
            RaycastHit2D player = Physics2D.BoxCast(transform.position, hurtArea, 0, new Vector2(0, 0), 0, playerMask);
            if (player)
            {
                PlayerCharacter.Instance.Damage(damage);
                hitCooldown = 0.3f;
            }

        }
        else
        {
            hitCooldown -= Time.deltaTime;
        }


        if (flashTime > 0)
        {
            flashHurt.enabled = true;
            flashTime -= Time.deltaTime;
        }
        else
        {
            flashHurt.enabled = false;
            if (health <= 0)
            {
                if (GetComponent<Uranium>() != null)
                    GetComponent<Uranium>().Kill();
                LevelGenerator.Instance.AddMoney(coinDrop);
                LevelGenerator.Instance.enemyCount--;
                Destroy(gameObject);
            }
        }
    }

    public void ExplosionDamage()
    {
        health -= .2f;
        flashTime = .2f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();
        if (other.gameObject.tag == "hurt" && enemyDamage != null)
        {
            health -= enemyDamage.GetDamage();
            flashTime = .2f;
            rb2D.AddForce(enemyDamage.GetPushBack(transform), ForceMode2D.Impulse);
            AudioManager.Instance.PlaySound(hurtSound, 1, 1);
        }
    }
    public void SetTarget(Vector2 target)
    {
        overidePosition = true;
        this.target = (Vector3)target;
    }

    public float GetHealth()
    {
        return health / originalHealth;
    }
    public void FollowPlayer()
    {
        overidePosition = false;
    }
    public void Heal(float amt)
    {
        if (health >= originalHealth)
            return;
        health += amt;
    }
}
