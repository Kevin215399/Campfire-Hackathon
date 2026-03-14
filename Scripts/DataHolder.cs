using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tools
{
    None,
    Shovel,
    Dynomite,
    Pickaxe,
    Mallet,
    Drill,
}
public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance { get; private set; }
    [SerializeField] private bool didDie = false;
    [SerializeField] private int money = 0;
    [SerializeField] private int dynomite = 0;
    [SerializeField] private float health = 0;
    [SerializeField] private Tools lastTool = Tools.None;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void SaveData()
    {
        if (PlayerCharacter.Instance.currentItem != null)
            lastTool = PlayerCharacter.Instance.currentItem.type;
        else
            lastTool = Tools.None;
        dynomite = LevelGenerator.Instance.dynomite;
        money = LevelGenerator.Instance.money;
        health = PlayerCharacter.Instance.health;
        if (health <= 0)
        {
            health = 100;
            money = (int)(money * .75f);
        }
    }
    public bool GetDidDie()
    {
        return didDie;
    }
    public int GetMoney()
    {
        return money;
    }
    public int GetDynomite()
    {
        return dynomite;
    }
    public float GetHealth()
    {
        return health;
    }
    public Tools GetTool()
    {
        return lastTool;
    }
}
