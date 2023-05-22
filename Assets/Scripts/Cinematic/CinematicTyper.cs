using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CinematicTyper : LocalManager<CinematicTyper>
{
    public bool isTyping;
    private float speed;
    public float Speed => speed;
    private Coroutine typingCoroutine;
    
    private CinematicLine currentDialogueLine;
    public CinematicLine CurrentDialogueLine;
    
    //gére le défilement du texte, prend en paramètre l'ui de texte, et une vitesse
    public IEnumerator DisplayLine(CinematicLine dialogueBox,TextMeshProUGUI textDialogue, float speed)
    {
        textDialogue.text = "";
        foreach (char letter in dialogueBox.text.ToCharArray())
        {
            isTyping = true;
            textDialogue.text += letter;
            yield return new WaitForSeconds(speed);
        }
        isTyping = false;
        UIIntro.Instance.onDisplayEndDelegate();
    }
    
    //gère le commencemencement du text
    public void StartTyping(CinematicLine dialogueBox,TextMeshProUGUI textDialogue, float speed)
    {
        typingCoroutine = StartCoroutine(DisplayLine(dialogueBox,textDialogue, speed));
        isTyping= true;
    }
    
    //Gère le fait de skip le text
    public void EndTyping(CinematicLine dialogueBox,TextMeshProUGUI textDialogue)
    {
        StopCoroutine(typingCoroutine);
        isTyping = false;
    }
}
