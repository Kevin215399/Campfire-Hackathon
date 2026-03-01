using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CloseElevator : MonoBehaviour
{
    [SerializeField] private Transform elevatorCage;
    [SerializeField] private float upPosition;
    [SerializeField] private float timer = 0;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Image fade;
    private bool open = false;
    private bool alreadyOpened = false;


    public void OpenCage()
    {
        timer = -1;
    }
    private void Update()
    {
        if (LevelGenerator.Instance.enemyCount <= 0 && !alreadyOpened)
        {
            open = true;
            alreadyOpened = true;
        }

        if (open)
        {
            elevatorCage.localPosition = new Vector2(0, Mathf.Lerp(0, upPosition, Mathf.Max(0, Mathf.Min(timer, 1))));
            if (timer < 1)
                timer += Time.deltaTime;

            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0, new Vector2(0, 0), 0, playerMask);
            if (hit)
            {
                open = false;
            }
        }
        else
        {
            elevatorCage.localPosition = new Vector2(0, Mathf.Lerp(0, upPosition, Mathf.Max(0, Mathf.Min(timer, 1))));
            if (timer > 0)
                timer -= Time.deltaTime;

            if (alreadyOpened)
            {
                fade.color = new Color(0, 0, 0, 1 - timer);
                if (timer <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
