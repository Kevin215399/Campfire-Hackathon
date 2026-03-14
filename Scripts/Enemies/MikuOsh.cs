using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikuOsh : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] directions = new SpriteRenderer[4];
    [SerializeField] private float threshold = 0.5f;

    [SerializeField] private Transform bar;
    [SerializeField] private ParticleSystem ps;
    private Rigidbody2D rb2D;
    [SerializeField] private float coolDown = 2;
    [SerializeField] private float animateBar = 0;
    [SerializeField] private bool attacking = false;
    [SerializeField] private float spinAnimate = 0;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        foreach (SpriteRenderer sr in directions)
        {
            sr.enabled = false;
        }
        Vector2 velocity = rb2D.velocity;
        velocity.Normalize();
        Debug.Log(velocity);
        if (velocity.y < -threshold)
        {
            directions[2].enabled = true;
        }
        else if (velocity.y > threshold)
        {
            directions[0].enabled = true;
        }
        else if (velocity.x < -threshold)
        {
            directions[3].enabled = true;
        }
        else if (velocity.x > threshold)
        {
            directions[1].enabled = true;
        }
        else
        {
            directions[2].enabled = true;
        }


        if (coolDown <= 0 && Vector2.Distance(transform.position, PlayerCharacter.Instance.transform.position) < 3)
        {
            coolDown = 6f;
            GetComponent<Enemy>().SetTarget(transform.position);
            animateBar = 0;
            spinAnimate = 0;
            attacking = true;
        }
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (attacking)
        {
            if (animateBar < 1)
            {
                animateBar += Time.deltaTime * 1.3f;
                bar.localScale = new Vector3(animateBar * 10, .2f, 1);
                ps.transform.localScale = new Vector3(animateBar, 1, 1);
            }
            else
            {

                if (spinAnimate < 3)
                {
                    bar.eulerAngles = new Vector3(0, 0, bar.eulerAngles.z + (Mathf.Sin(spinAnimate * 3.14f / 180 * 120 - 90) + 1) * Time.deltaTime * 800);
                    ps.transform.eulerAngles = new Vector3(0, 0, bar.eulerAngles.z + (Mathf.Sin(spinAnimate * 3.14f / 180 * 120 - 90) + 1) * Time.deltaTime * 800);
                    spinAnimate += Time.deltaTime;
                }
                else
                {
                    if (animateBar < 2)
                    {
                        animateBar += Time.deltaTime * 1.3f;
                        bar.localScale = new Vector3((-animateBar + 2) * 10, .2f, 1);
                        ps.transform.localScale = new Vector3(-animateBar + 2, 1, 1);
                    }
                    else
                    {
                        GetComponent<Enemy>().FollowPlayer();
                        attacking = false;
                    }

                }
            }
        }

    }
}
