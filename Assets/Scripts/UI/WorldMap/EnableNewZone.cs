using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNewZone : MonoBehaviour
{
    [SerializeField] private bool unlockPlace;
    [SerializeField] private bool unlockHangars;
    [SerializeField] private bool unlockFactories;
    [SerializeField] private bool unlockMarket;
    private void OnEnable()
    {
        if(unlockPlace)
        {
            UIWorldMap.Instance.placeUnlocked = true;
        }

        else if(unlockHangars)
        {
            UIWorldMap.Instance.hangarsUnlocked = true;
        } 
        
        else if(unlockFactories)
        {
            UIWorldMap.Instance.factoriesUnlocked = true;
        }

        else if(unlockMarket)
        {
            UIWorldMap.Instance.marketUnlocked = true;
        }
    }
}
