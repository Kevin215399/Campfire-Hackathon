using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayMan : MonoBehaviour
{
    [SerializeField] private GameObject clump;
    [SerializeField] private float stopDistance;

    private float throwTime = 0;
    private void Update()
    {
        Vector2 playerPos = PlayerCharacter.Instance.transform.position;
        if (Vector2.Distance(transform.position, playerPos) < stopDistance)
        {
            throwTime -= Time.deltaTime;
            if (throwTime < 0)
            {
                throwTime = 1f;
                GameObject x = Instantiate(clump);
                x.transform.position = transform.position;
            }
        } else if (throwTime > 0)
        {
            throwTime -= Time.deltaTime;
        }
    }
}
