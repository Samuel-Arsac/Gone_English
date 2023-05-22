using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string soundtostring;

    private Present choice;
    
    /*public void InitChoiceButton(Present choice)
    {
        this.choice = choice;
        text.text = choice.presentDialogue;
    }

    /*public void OnClick()
    {
        UIManager.Instance.OnClickChoiceButton(choice.choiceidx);
        sound = UnityEngine.Random.Range(1, 5);
        soundtostring = sound.ToString();
        FindObjectOfType<AudioManager>().Play(soundtostring);
    }*/
}
