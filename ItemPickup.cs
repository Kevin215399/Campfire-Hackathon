using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    [SerializeField] private BoxCollider2D itemCollider;
    [SerializeField] private GameObject container;
    private Vector2 roomWhereItIs;
    public bool isHeld { get; private set; }
    private Rigidbody2D rb2D;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void Pickup()
    {
        isHeld = true;
        rb2D.isKinematic = true;
        transform.parent = PlayerCharacter.Instance.transform;
        transform.localPosition = new Vector3(0, 0, 0);
        if (itemCollider != null)
            itemCollider.enabled = false;
    }
    public void Drop()
    {
        isHeld = false;
        rb2D.isKinematic = false;
        transform.parent = null;
        if (itemCollider != null)
            itemCollider.enabled = true;
    }
    public void SetRoom(Vector2 room)
    {
        roomWhereItIs = room;
    }
    private void Update()
    {
        if (isHeld)
        {
            roomWhereItIs = LevelGenerator.Instance.currentRoom;
            container.SetActive(true);
        }
        else
        {
            container.SetActive(roomWhereItIs == LevelGenerator.Instance.currentRoom);
            if (roomWhereItIs == LevelGenerator.Instance.currentRoom)
            {
                rb2D.isKinematic = false;
                if (itemCollider != null)
                    itemCollider.enabled = true;
            }
            else
            {
                rb2D.isKinematic = true;
                if (itemCollider != null)
                    itemCollider.enabled = false;
            }
        }
    }
    public void DestroyCollider()
    {
        if (itemCollider != null)
            itemCollider.enabled = false;
        itemCollider = null;
    }
}
