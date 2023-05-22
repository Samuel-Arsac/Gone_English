using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMusic : MonoBehaviour
{
    [SerializeField] private string songName;
    private void Start() 
    {
        AudioManager.Instance.PlayMusic(songName);
    }
}
