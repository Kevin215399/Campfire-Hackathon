using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clay : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float clamp;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private LayerMask playerMask;
    private Vector2 target;
    private float fadeTime;
    private void Start()
    {
        target = PlayerCharacter.Instance.transform.position; 
        fadeTime = 3;
    }
    private void Update()
    {
        Vector2 move = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        move *= speed;
        move.x = Mathf.Min(move.x, clamp);
        move.y = Mathf.Min(move.y, clamp);
        move *= Time.deltaTime;
        transform.position += new Vector3(move.x, move.y, 0);

        fadeTime-=Time.deltaTime;
        sprite.color = new Color(1,1,1,Mathf.Min(fadeTime, 1));
        if (fadeTime <= 0)
        {
            Destroy(gameObject);
        }

        RaycastHit2D player = Physics2D.BoxCast(transform.position, new Vector2(.6f,.6f), 0, new Vector2(0,0), 0, playerMask);
        if (player)
        {
            PlayerCharacter.Instance.Damage(damage);
            Destroy(gameObject);
        }
    }
    
}
