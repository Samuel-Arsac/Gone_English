using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWatch : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItem;

    private void Start()
    {
        UIManager.Instance.EnableWatch();
        UIManager.Instance.CanUseWatch();
    }
}
