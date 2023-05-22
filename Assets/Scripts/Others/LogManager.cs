using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> logSections;
    [SerializeField]private List<TextMeshProUGUI> logText;

    [SerializeField] private List<Image> characterPreviewSprite;


    private void OnEnable()
    {
        //logText.text = DialogueHandler.Instance.characterInfo.dialogueList[DialogueHandler.Instance.currentDialogueIdx].text;

        for(int i = 0; i < logText.Count; i++)
        {
            if(DialogueHandler.Instance.currentDialogueIdx - i < 0)
            {
                logSections[i].SetActive(false);
            }
            else
            {
                logSections[i].SetActive(true);
                logText[i].text = DialogueHandler.Instance.characterInfo.dialogueList[DialogueHandler.Instance.currentDialogueIdx - i].text;
            }
            
        }
    }
}
