using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image fade;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        fade.color = new Color(0, 0, 0, 1 - timer);
        if (timer > 1)
            Destroy(this);
    }
}
