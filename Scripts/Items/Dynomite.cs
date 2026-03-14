using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynomite : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] animationSteps;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip tickSound;
    private int explosionStep = 0;
    private float explosionTimer = 0;
    private bool isExploding = false;
    private float doDamage = 0;
    private float tickTime = 0;
    private void Update()
    {
        if (isExploding)
        {
            explosionTimer -= Time.deltaTime;
            if (explosionTimer <= 0)
            {
                explosionTimer = .1f;
                if (explosionStep < animationSteps.Length)
                    animationSteps[explosionStep].SetActive(false);
                explosionStep++;
                if (explosionStep == animationSteps.Length)
                {
                    AudioManager.Instance.PlaySound(explosionSound, 1, 1);
                    explosionTimer = 3f;
                    explosion.Play();
                    doDamage = 1f;
                }
                else if (explosionStep >= animationSteps.Length + 1)
                {
                    Destroy(gameObject);
                }
                else
                {
                    animationSteps[explosionStep].SetActive(true);
                }
            }
            if (explosionStep < animationSteps.Length)
            {
                if (tickTime <= 0)
                {
                    tickTime = .1f;
                    AudioManager.Instance.PlaySound(tickSound, .7f, 1);
                }
                else
                {
                    tickTime -= Time.deltaTime;
                }
            }
        }
        if (doDamage > 0)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1.5f, new Vector2(0, 0), 0);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.GetComponent<Enemy>() != null)
                    hit.transform.GetComponent<Enemy>().ExplosionDamage();
            }
            doDamage -= Time.deltaTime;
        }
    }
    public void StartClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDynomite = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        PlayerCharacter.Instance.Drop();
        GetComponent<ItemPickup>().DestroyCollider();
        throwDynomite.Normalize();
        throwDynomite *= 200;
        GetComponent<Rigidbody2D>().AddForce(throwDynomite, ForceMode2D.Impulse);
        explosionTimer = .5f;
        isExploding = true;
    }
    public void StopClick()
    {
        return;
    }
}
