using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class DialogueLine

{
    public DialogueType type =  DialogueType.Dialogue;
    public bool changementAfterDialogue = false;
    [ShowIf("changementAfterDialogue")][AllowNesting]public GameObject objectsAfterChangement;
    [TextArea][Multiline] public string text = "";

    public bool noNames = false;
    public bool unknownCharacter = false;
    public bool showingObject = false;

    public bool playSound;
    [ShowIf("playSound")][AllowNesting] public string soundToPlay;

    public bool playDialogue;
    [ShowIf("playDialogue")] [AllowNesting] public string dialogueToPlay;

    public bool isPetraHere;
    public bool isFirstInterlocutorHere;
    public bool isPetraTalking;
    [ShowIf("isPetraTalking")][AllowNesting]
    public bool isPetraThinking;
    [HideIf("isPetraTalking")][AllowNesting]
    public bool isThirdTalking;
    public int nextDialogueIdx = 0;
    public int characterToTalkSpriteIdx;
    public bool IsOtherInterlocutorHere;
    [ShowIf("IsOtherInterlocutorHere")][AllowNesting]public int otherInterlocutorSpriteIdx;
    public int petraSpriteIdx;
    public enum DialogueType{Dialogue = 0, Present = 1, EndDialogue = 2, EndMonologue = 3}
}

[System.Serializable]
public struct Present
{
    public string objectToProgress;
    public int dialogueToGo;
}
