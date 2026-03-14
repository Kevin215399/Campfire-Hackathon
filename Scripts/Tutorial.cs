using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }
    [SerializeField] private TMP_Text[] tutorialTexts;
    [SerializeField] private GameObject allowExit;
    private int stage = -1;
    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        for (int i = 0; i < tutorialTexts.Length; i++)
            tutorialTexts[i].color = new Color(1, 1, 1, Mathf.Min(2.5f, tutorialTexts[i].color.a + (i == stage ? 1 : 0 - tutorialTexts[i].color.a) * Time.deltaTime * 5));

        switch (stage)
        {
            case 0:
                if (PlayerCharacter.Instance.hasMoved)
                    stage++;
                break;
            case 1:
                if (PlayerCharacter.Instance.currentItem != null)
                    stage++;
                break;
            case 2:
                if (PlayerCharacter.Instance.hasUsedItem)
                    stage++;
                break;
            case 3:
                allowExit.SetActive(false);
                break;
        }

    }
    public void StartTutorial()
    {
        stage = 0;
    }
}
