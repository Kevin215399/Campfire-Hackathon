using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{

    [SerializeField] private GameObject[] minerals;
    public void SetMineral(int type)
    {
        for (int i = 0; i < minerals.Length; i++)
            minerals[i].SetActive(i==type);
    }
}
