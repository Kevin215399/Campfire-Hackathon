using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private GameObject unfilledTile;
    [SerializeField] private GameObject filledTile;
    public Vector2 position;

    private void Start()
    {
        LevelGenerator.OnChangeRoom += SetFilled;
    }
    private void OnDestroy()
    {
        LevelGenerator.OnChangeRoom -= SetFilled;
    }
    private void SetFilled()
    {
        if (position != LevelGenerator.Instance.currentRoom)
            return;
        unfilledTile.SetActive(false);
        filledTile.SetActive(true);
    }
}
