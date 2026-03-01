using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraAdjust : MonoBehaviour
{
    public float targetWidth = 10f;  // gameplay area width
    public float targetHeight = 5f;  // gameplay area height


    private void Update()
    {

        Camera cam = Camera.main;
        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = targetWidth / targetHeight;

        float newSize;
        if (screenAspect >= targetAspect)
        {
            newSize = targetHeight / 2f;
        }
        else
        {
            newSize = (targetWidth / 2f) / screenAspect;
        }

        Camera.main.orthographicSize = newSize;
    }
}
