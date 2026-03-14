using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Vector2 size;
    [SerializeField] private int moveDirection;
    public static List<Door> Instances = new List<Door>();
    private bool canDoor = false;
    private float delay = .2f;
    private void Start()
    {
        Instances.Add(this);
    }
    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, size, 0, new Vector2(0, 0), 0, playerMask);
        if (hit)
        {

            if (canDoor)
                LevelGenerator.Instance.Move(moveDirection);
        }
        else
        {
            canDoor = true;
        }
    }
    public void StopDooring()
    {
        canDoor = false;
        delay = .3f;
    }
}
