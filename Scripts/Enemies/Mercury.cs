using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercury : MonoBehaviour
{
    [SerializeField] private float radius;
    private float healTime = 0;
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void Update()
    {

        float lowestHealth = 1;
        Vector2 target = Vector2.zero;
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("enemy"))
        {
            if (x.GetComponent<Mercury>() != null)
                continue;
            float health = x.GetComponent<Enemy>().GetHealth();
            if (health <= lowestHealth)
            {
                lowestHealth = health;
                target = x.transform.position;
            }

            if (healTime <= 0 && Vector2.Distance(transform.position, x.transform.position) < radius)
            {
                x.GetComponent<Enemy>().Heal(2f);
            }
        }
        if (lowestHealth == 1)
            enemy.FollowPlayer();
        else
            enemy.SetTarget(target);

        if (healTime <= 0)
        {
            healTime = 0.7f;
        }
        healTime -= Time.deltaTime;

    }

}
