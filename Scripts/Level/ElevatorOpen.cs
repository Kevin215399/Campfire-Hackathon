using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOpen : MonoBehaviour
{
    public static ElevatorOpen Instance { get; private set; }
    [SerializeField] private Transform elevatorCage;
    [SerializeField] private float upPosition;
    [SerializeField] private float timer = 0;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenCage()
    {
        timer = -1;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        elevatorCage.localPosition = new Vector2(0, Mathf.Lerp(0, upPosition, Mathf.Max(0, Mathf.Min(timer, 1))));
    }
}
