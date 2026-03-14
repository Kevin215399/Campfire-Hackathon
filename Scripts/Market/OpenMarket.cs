using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenMarket : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform market;
    [SerializeField] private Image darken;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Vector2 openAreaSize;

    [SerializeField] private float enterP;
    [SerializeField] private float enterD;

    [SerializeField] private float leaveAccel;
    private float lastError;
    private bool canOpen = false;
    private float animate = 0;
    private bool isOpen = false;
    private float velocity = 0;
    private void Update()
    {
        RaycastHit2D player = Physics2D.BoxCast(transform.position, openAreaSize, 0, Vector2.zero, 0, playerMask);
        if (player)
        {
            if (canOpen)
            {
                isOpen = true;
                lastError = 0;

                market.anchoredPosition = new Vector2(0, canvasRect.rect.height / 2 + market.rect.height / 2);
                market.gameObject.SetActive(true);
                velocity = 0;
            }
            canOpen = false;
        }
        else
        {
            isOpen = false;
            canOpen = true;
        }

        if (isOpen)
        {
            if (animate < 1f)
                animate += Time.deltaTime;

            float error = -market.anchoredPosition.y;
            float p = error * enterP;
            float d = (error - lastError) * enterD;
            lastError = error;
            velocity += (p + d) * Time.deltaTime;

            market.anchoredPosition += new Vector2(0, velocity);

        }
        else
        {
            if (animate > 0)
                animate -= Time.deltaTime;

            velocity += leaveAccel * Time.deltaTime;


        }
        Debug.Log(Time.deltaTime);
        market.anchoredPosition += new Vector2(0, velocity * Time.deltaTime);

        darken.color = new Color(0, 0, 0, Mathf.Min(animate, 1) * .7f);
    }
    public void CloseMarket()
    {
        isOpen = false;
    }
}
