using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreData : MonoBehaviour
{
    private void Update()
    {
        if (DataHolder.Instance == null)
            return;
        LevelGenerator.Instance.AddMoney(DataHolder.Instance.GetMoney());
        PlayerCharacter.Instance.SetHealth(DataHolder.Instance.GetHealth());
        LevelGenerator.Instance.GivePlayerWeapon(DataHolder.Instance.GetTool());
        LevelGenerator.Instance.SetDynomite(DataHolder.Instance.GetDynomite());
        Destroy(this);
    }
}
