using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranium : MonoBehaviour
{
    [SerializeField] private GameObject radioactivity;
    [SerializeField] private Transform particleObj;

    private float spawnTime;
    public void Kill()
    {
        particleObj.parent = null;
    }
    private void Update()
    {
        if (spawnTime < 0)
        {
            GameObject x = Instantiate(radioactivity);
            x.transform.position = transform.position;
            spawnTime = .3f;
        } else
        {
            spawnTime-=Time.deltaTime;
        }
    }
}
