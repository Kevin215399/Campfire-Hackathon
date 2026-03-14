using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] private float despawnTime;

    private void Update()
    {
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0)
            Destroy(gameObject);
    }
}
