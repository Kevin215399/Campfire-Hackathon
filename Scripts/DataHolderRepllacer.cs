using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolderRepllacer : MonoBehaviour
{
    [SerializeField] private GameObject dataHolder;
    private void Start()
    {
        if (DataHolder.Instance == null)
        {
            Instantiate(dataHolder);
        }
    }
}
