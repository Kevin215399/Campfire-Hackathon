using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianMan : MonoBehaviour
{

    private float coolDown = 0;
    private void Update()
    {
        Vector2 playerPos = PlayerCharacter.Instance.transform.position;
        if (coolDown < 0 && Vector2.Distance(transform.position, playerPos) < 5)
        {
            Debug.Log("super push");
            Vector2 push = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
            push *= 100;
            //push *= Time.deltaTime;

            GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);
            coolDown = 2f;

        }
        if (coolDown > 0)
            coolDown -= Time.deltaTime;


    }


}
