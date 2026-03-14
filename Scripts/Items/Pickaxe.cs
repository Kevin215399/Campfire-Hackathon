using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour, IInteractable
{
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Transform container;
    private Vector2 start;
    private Vector2 target;
    private float throwTime;
    private bool isThrow;
    private void Start()
    {
        LevelGenerator.OnChangeRoom += ChangeRoom;
    }
    private void OnDestroy()
    {
        LevelGenerator.OnChangeRoom -= ChangeRoom;
    }
    private void Update()
    {
        if (isThrow)
        {
            trail.emitting = true;
            throwTime += Time.deltaTime;
            container.eulerAngles += new Vector3(0, 0, Time.deltaTime * 360 * 5);
            transform.position = Vector2.Lerp(throwTime < 0.5f ? start : PlayerCharacter.Instance.transform.position, target, (1 - Mathf.Cos(throwTime * 360 * 3.1415f / 180)) / 2);
            if (throwTime >= 1)
            {
                isThrow = false;
            }
        }
        else
        {
            trail.emitting = false;
            container.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void StartClick()
    {
        if (isThrow)
            return;
        start = transform.position;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        target.Normalize();
        target *= 8;
        target += (Vector2)transform.position;

        isThrow = true;
        throwTime = 0;
    }
    public void StopClick()
    {
        return;
    }
    private void ChangeRoom()
    {
        trail.Clear();
    }
}
