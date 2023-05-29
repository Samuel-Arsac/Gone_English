using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMusic : MonoBehaviour
{
    [SerializeField] private string songName;
    private void Start() 
    {
        if(AudioManager.Instance.newTrack.name ==  songName)
        {
            return;
        }
        else
        {
            AudioManager.Instance.PlayMusic(songName);
        }
        
    }
}
