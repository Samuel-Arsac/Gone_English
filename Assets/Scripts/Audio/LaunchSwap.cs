using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSwap : MonoBehaviour
{
    [SerializeField] private string songName;
    [SerializeField] private bool comeFromMenu;
    private void Start() 
    {
        if(comeFromMenu)
        {
            AudioManager.Instance.SwapMusic(songName);
        }
        else
        {
            AudioManager.Instance.PlayMusic(songName);
        }
        
    }
}
