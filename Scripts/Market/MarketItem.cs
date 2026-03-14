using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum MarketItemType
{
    dynomite,
    compass,
    antidote,
    shovel,
    pickaxe,
    mallet,
    drill

};
public class MarketItem : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int price;
    [SerializeField] private MarketItemType type;
    [SerializeField] private AudioClip buySound;


    private void Update()
    {
        priceText.text = "$" + price.ToString();
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
        AudioManager.Instance.PlaySound(buySound,1,1);
        LevelGenerator.Instance.AddMoney(-price);
        switch (type)
        {
            case MarketItemType.dynomite:
                LevelGenerator.Instance.BuyDynomite();
                break;
            case MarketItemType.compass:
                LevelGenerator.Instance.ShowElevator();
                break;
            case MarketItemType.antidote:
                PlayerCharacter.Instance.AddHealth(25);
                break;
            case MarketItemType.shovel:
                LevelGenerator.Instance.GivePlayerWeapon(Tools.Shovel);
                break;
            case MarketItemType.mallet:
                LevelGenerator.Instance.GivePlayerWeapon(Tools.Mallet);
                break;
            case MarketItemType.pickaxe:
                LevelGenerator.Instance.GivePlayerWeapon(Tools.Pickaxe);
                break;
            case MarketItemType.drill:
                LevelGenerator.Instance.GivePlayerWeapon(Tools.Drill);
                break;
        }
    }
}
