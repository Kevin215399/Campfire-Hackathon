using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private GameObject unfilledTile;
    [SerializeField] private GameObject filledTile;
    [SerializeField] private GameObject elevatorTile;
    public Vector2 position;

    private void Start()
    {
        LevelGenerator.OnChangeRoom += SetFilled;
        LevelGenerator.OnShowElevator += ShowElevator;
    }
    private void OnDestroy()
    {
        LevelGenerator.OnChangeRoom -= SetFilled;
        LevelGenerator.OnShowElevator -= ShowElevator;
    }
    private void SetFilled()
    {
        if (position != LevelGenerator.Instance.currentRoom)
            return;
        unfilledTile.SetActive(false);
        filledTile.SetActive(true);
    }
    private void ShowElevator()
    {
        if(LevelGenerator.Instance.endRoom == position)
        {
            elevatorTile.SetActive(true);
            unfilledTile.SetActive(false);
        }
    }
}
