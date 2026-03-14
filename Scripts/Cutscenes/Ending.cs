using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    [SerializeField] private GameObject text3;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private SpriteRenderer rightJosh;
    [SerializeField] private SpriteRenderer downJosh;
    [SerializeField] private Vector2 endpoint;
    private float timer = 3;
    private int stage = 0;
    private float lerpTime = 0;
    private Vector2 start;

    private void Start()
    {
        start = transform.position;
    }

    private void Update()
    {
        if (timer < 0)
        {
            stage++;
            switch (stage)
            {
                case 0:
                    timer = 2;
                    break;
                case 1:
                    timer = 2.5f;
                    break;
                case 2:
                    rightJosh.enabled = false;
                    downJosh.enabled = true;
                    timer = 1.5f;
                    break;
                case 3:
                    text1.SetActive(true);
                    timer = 2.5f;
                    break;
                case 4:
                    text2.SetActive(true);
                    timer = 2.5f;
                    break;
                case 5:
                    text3.SetActive(true);
                    timer = 3f;
                    break;
                case 6:
                    downJosh.enabled = false;
                    ps.Play();
                    GetComponent<AudioSource>().Play();
                    timer = 1.5f;
                    break;
                default:
                    timer = 1;
                    break;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (stage == 1)
        {
            lerpTime += Time.deltaTime;
            transform.position = Vector2.Lerp(start, endpoint, Mathf.Min(Mathf.Max(lerpTime, 0), 1));
        }
    }
}
