using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRandomizer : MonoBehaviour
{
    public static GroundRandomizer Instance {get; private set;}
    private void Start()
    {
        Instance = this;
        Randomize();
    }
    public void Randomize()
    {
        transform.position = new Vector3(Mathf.Floor(Random.Range(-7, 7)), Mathf.Floor(Random.Range(-7, 7)), 0);
    }
}
