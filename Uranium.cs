using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranium : MonoBehaviour
{
    [SerializeField] private GameObject radioactivity;

    private float spawnTime;
    private void Update()
    {
        if (spawnTime < 0)
        {
            GameObject x = Instantiate(radioactivity);
            x.transform.position = transform.position;
            spawnTime = .1f;
        } else
        {
            spawnTime-=Time.deltaTime;
        }
    }
}
