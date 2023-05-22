using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{

    public void PlaySound(string sound)
    {
        AudioManager.Instance.PlaySFX(sound);
    }

}
