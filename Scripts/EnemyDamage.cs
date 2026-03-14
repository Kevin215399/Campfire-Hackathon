using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float pushBack;
    [SerializeField] private float damage;
    [SerializeField] private Transform anchor;

    public float GetDamage()
    {
        return damage;
    }
    public Vector2 GetPushBack(Transform enemyTransform)
    {
        if (pushBack == 0)
            return Vector2.zero;
        Vector2 output = enemyTransform.position - anchor.position;
        output.Normalize();
        output *= pushBack;
        return output;
    }
}
