using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearOnChange : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private void Start()
    {
        LevelGenerator.OnChangeRoom += Delete;
    }
    private void OnDestroy()
    {
        LevelGenerator.OnChangeRoom -= Delete;
    }
    private void Delete()
    {
        if (ps != null)
            ps.Clear();
        Destroy(gameObject);
    }
}
