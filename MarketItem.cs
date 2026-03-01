using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MarketItem : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int price;
    [SerializeField] private Transform container;


    private void Update()
    {
        if (LevelGenerator.Instance.money >= price)
        {
            priceText.color = Color.white;
        }
        else
        {
            priceText.color = Color.red;
        }

    }
    public void Press()
    {
        if (LevelGenerator.Instance.money < price)
            return;
        LevelGenerator.Instance.AddMoney(-price);
        LevelGenerator.Instance.BuyDynomite();
    }
}
