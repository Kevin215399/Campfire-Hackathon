using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianMan : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private GameObject obstacleCollider;
    [SerializeField] private int normalCollisionLayer;
    [SerializeField] private int ignoreRockCollisionLayer;
    private float coolDown = 2;
    private bool jumping = false;
    private float jumpPause = 0;
    private Vector2 target;
    private float returnCollider;
    private void Update()
    {
        Vector2 playerPos = PlayerCharacter.Instance.transform.position;

        if (coolDown <= 0 && Vector2.Distance(transform.position, playerPos) < 5)
        {
            jumpPause = .5f;
            jumping = true;
            coolDown = 3;
            target = playerPos;
            GetComponent<Enemy>().SetTarget(transform.position);
            ps.Play();
            sprites[1].enabled = true;
            sprites[0].enabled = false;
        }

        if (jumping)
        {
            if (jumpPause <= 0)
            {
                obstacleCollider.layer = ignoreRockCollisionLayer;
                Debug.Log("super push");
                Vector2 push = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
                push *= 140;
                //push *= Time.deltaTime;

                GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);
                coolDown = 2f;

                jumping = false;

                GetComponent<Enemy>().FollowPlayer();

                sprites[0].enabled = true;
                sprites[1].enabled = false;

                returnCollider = .4f;
            }
            else
            {
                jumpPause -= Time.deltaTime;
            }
        }
        if (coolDown > 0)
            coolDown -= Time.deltaTime;

        if (returnCollider > 0)
        {
            returnCollider -= Time.deltaTime;
        }
        else
        {
            obstacleCollider.layer = normalCollisionLayer;
        }


    }


}
