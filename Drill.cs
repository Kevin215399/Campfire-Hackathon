using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour, IInteractable
{
    public void StartClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDynomite = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        PlayerCharacter.Instance.Drop();
        throwDynomite.Normalize();
        throwDynomite *= 15;
        GetComponent<Rigidbody2D>().AddForce(throwDynomite, ForceMode2D.Impulse);
    }
    public void StopClick()
    {
        return;
    }
}
