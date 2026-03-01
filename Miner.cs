using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    private float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime / 20;
        transform.localScale = new Vector2(1, 1 + Mathf.Sin(timer * 180 / 3.1415f) / 12);
    }
}
