using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private CharacterInfo characterInfo;
    [SerializeField] private bool enviroExamen;
    // Start is called before the first frame update
    void Start()
    { 
        if(enviroExamen)
        {
            UIManager.Instance.SetPostProcess();
        }
        UIManager.Instance.DisableInteractionEnvironnment();
        characterInfo = GetComponent<CharacterInfo>();
        if (characterInfo.autoLaunch)
        {
            StartCoroutine(characterInfo.waitForDialogueToStart());
        }
    }
}
