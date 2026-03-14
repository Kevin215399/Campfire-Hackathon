using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class StartAnimation : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text skipText;
    [SerializeField] private Transform rotatePlayer;
    [SerializeField] private InputActionReference skipButton;
    [SerializeField] private Color normalSkipColor;

    private float animate = 0;
    private float stage = 0;
    private float skip = 0;
    private void Update()
    {
        if (animate <= 0)
        {
            switch (stage)
            {
                case 0:
                    animate = 2f;
                    PlayerCharacter.Instance.Freeze();
                    break;
                case 1:
                    textBox.SetActive(true);
                    text.text = "wha... what happend?";
                    animate = 2;
                    break;
                case 2:
                    animate = 2f;
                    break;
                case 3:
                    PlayerCharacter.Instance.transform.parent = null;
                    Destroy(rotatePlayer.gameObject);
                    break;
                case 4:
                    text.text = "Where am I?";
                    animate = 2f;
                    break;
                case 5:
                    text.fontSize = 40;
                    text.text = "MY DRILL!!!!";
                    animate = 3f;
                    break;
                case 6:
                    text.fontSize = 20;
                    text.text = "I got to get out of here...";
                    animate = 3f;
                    break;
                case 7:
                    text.text = "I can take the elevators to the surface";
                    animate = 3.5f;
                    break;
                case 8:
                    textBox.SetActive(false);
                    PlayerCharacter.Instance.Unfreeze();
                    PlayerCharacter.Instance.ShowHealthBar();
                    Tutorial.Instance.StartTutorial();

                    Destroy(this);
                    break;

            }
            stage++;
        }
        animate -= Time.deltaTime;
        if (stage == 3)
        {
            rotatePlayer.eulerAngles = new Vector3(0, 0, Mathf.Lerp(90, 0, Mathf.Max(0, 1 - animate)));
        }
        if (stage < 8 && skipButton.action.ReadValue<float>() == 1)
        {
            skip += Time.deltaTime;
            skipText.color = Color.white;
            if (skip > 1)
            {


                if (rotatePlayer != null)
                {
                    rotatePlayer.eulerAngles = new Vector3(0, 0, 0);
                    PlayerCharacter.Instance.transform.parent = null;
                    Destroy(rotatePlayer.gameObject);
                }
                stage = 8;
                animate = 0;

            }
        }
        else
        {
            skipText.color = normalSkipColor;
            skip = 0;
        }
        if (stage == 6)
        {
            text.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
        }
        else
        {
            text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }
}
