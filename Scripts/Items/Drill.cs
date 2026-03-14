using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject colliderObj;
    [SerializeField] private int ignoreMask;
    [SerializeField] private int collideMask;

    private float disableCollider = 0;
    private void Update()
    {
  

        if (disableCollider > 0)
        {
            disableCollider -= Time.deltaTime;
            colliderObj.layer = ignoreMask;
        }
        else
        {
            colliderObj.layer = collideMask;
        }
    }
    public void StartClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDrill = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        colliderObj.layer = ignoreMask;

        PlayerCharacter.Instance.Drop();
        throwDrill.Normalize();
        throwDrill *= 35;
        GetComponent<Rigidbody2D>().AddForce(throwDrill, ForceMode2D.Impulse);

        disableCollider = .2f;
    }
    public void StopClick()
    {
        return;
    }
}
