using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private Transform visual;
    [SerializeField] private float stopDistance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private SpriteRenderer flashHurt;
    private Rigidbody2D rb2D;
    private float time;
    private float hitCooldown = .3f;
    private float flashTime = 0;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, PlayerCharacter.Instance.transform.position) > stopDistance)
        {
            Vector2 velocity = PlayerCharacter.Instance.transform.position - transform.position;
            velocity.Normalize();
            velocity *= speed;

            rb2D.AddForce(velocity, ForceMode2D.Impulse);

            visual.localPosition = new Vector2(0, Mathf.Abs(Mathf.Cos(time * 10)) / 4);
            visual.localScale = new Vector2(1 + Mathf.Abs(Mathf.Cos(time * 10)) / 8, 1);
            time += Time.deltaTime / 3;
        }
        else
        {
            rb2D.velocity = new Vector2(0, 0);
        }


        if (hitCooldown < 0)
        {
            RaycastHit2D player = Physics2D.BoxCast(transform.position, new Vector2(.6f, .6f), 0, new Vector2(0, 0), 0, playerMask);
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

        if (health <= 0)
        {
            LevelGenerator.Instance.AddMoney(1);
            LevelGenerator.Instance.enemyCount--;
            Destroy(gameObject);
        }
        if (flashTime > 0)
        {
            flashHurt.enabled = true;
            flashTime -= Time.deltaTime;
        }
        else
        {
            flashHurt.enabled = false;
        }
    }

    public void ExplosionDamage()
    {
        health -= .2f;
        flashTime = .2f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "hurt5")
        {
            health -= 8;
            flashTime = .2f;
        }
    }
    void OnParticleCollision(GameObject particle)
    {
        Debug.Log("HIT PARTICLE");
    }

}
