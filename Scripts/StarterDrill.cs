using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterDrill : MonoBehaviour
{
    [SerializeField] private GameObject drill;
    private void Start()
    {
        LevelGenerator.OnChangeRoom += ChangeRoom;
    }
    private void OnDestroy()
    {
        LevelGenerator.OnChangeRoom -= ChangeRoom;
    }
    private void ChangeRoom()
    {
        drill.SetActive(LevelGenerator.Instance.currentRoom == new Vector2(0, 0));
    }
}
