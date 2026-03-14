using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform swipeMarker;
    [SerializeField] private float speed;
    [SerializeField] private Collider2D hurtCollider;
    
    private bool spin = false;

    [SerializeField] private List<Vector3> positions = new List<Vector3>();
    [SerializeField] private float trailSampleTime = 0.02f;
    [SerializeField] private int maxTrailSamples = 8;

    private float lineSampleTime = 0.1f;
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
            transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * speed);
            hurtCollider.enabled = true;

            if (lineSampleTime <= 0)
            {
                line.transform.eulerAngles = new Vector3(0,0,0);
                positions.Insert(0, swipeMarker.position-transform.position);
                if (positions.Count > maxTrailSamples)
                {
                    positions.RemoveAt(positions.Count - 1);
                }
                line.positionCount = positions.Count;
                line.SetPositions(positions.ToArray());
                lineSampleTime = trailSampleTime;
            }
            else
            {
                lineSampleTime -= Time.deltaTime;
            }
        }
        else
        {
            hurtCollider.enabled = false;

            if (lineSampleTime <= 0)
            {

                if (positions.Count > 0)
                {
                    positions.RemoveAt(positions.Count - 1);
                    line.positionCount = positions.Count;
                    line.SetPositions(positions.ToArray());
                }
                lineSampleTime = 0.02f;
            }
            else
            {
                lineSampleTime -= Time.deltaTime;
            }
        }
    }
}
