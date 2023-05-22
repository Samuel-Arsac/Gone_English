using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyper : ProjectManager<TextTyper>
{
    [HideInInspector] public bool isTyping;
    private float speed;
    public float Speed => speed;
    private Coroutine typingCoroutine;
    
    private DialogueLine currentDialogueLine;
    [HideInInspector] public DialogueLine CurrentDialogueLine; //=> currentDialogueLine;
    //public readonly string NBSP = " ";

    //gére le défilement du texte, prend en paramètre l'ui de texte, et une vitesse
    public IEnumerator DisplayLine(DialogueLine dialogueBox,TextMeshProUGUI textDialogue, float speed)
    {
        textDialogue.text = "";
        foreach (char letter in dialogueBox.text.ToCharArray())
        {
            isTyping = true;
            textDialogue.text += letter;
            yield return new WaitForSeconds(speed);
        }
        isTyping = false;
        UIManager.Instance.onTextDisplayEnd();
    }
    
    //gère le commencemencement du text
    public void StartTyping(DialogueLine dialogueBox,TextMeshProUGUI textDialogue, float speed)
    {
        typingCoroutine = StartCoroutine(DisplayLine(dialogueBox,textDialogue, speed));
        isTyping= true;
    }
    
    //Gère le fait de skip le text
    public void EndTyping(DialogueLine dialogueBox,TextMeshProUGUI textDialogue)
    {
        StopCoroutine(typingCoroutine);
        isTyping = false;
    }
}
