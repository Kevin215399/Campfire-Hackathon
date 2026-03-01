using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    [SerializeField] private float speed;
    [SerializeField] private Collider2D hurtCollider;
    private bool spin = false;

    public void StartClick()
    {
        spin = true;
    }
    public void StopClick()
    {
        spin = false;
    }
    public void Update()
    {
        if (spin)
        {
            Debug.Log("spin");
            transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
            hurtCollider.enabled = true;
        }
        else
        {
            hurtCollider.enabled = false;
        }
    }
}
