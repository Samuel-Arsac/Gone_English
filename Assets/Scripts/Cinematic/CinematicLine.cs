using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class CinematicLine

{
    public DialogueType type =  DialogueType.Dialogue;
    public bool changeIllustration;
    [Multiline] public string text = "";
    public bool playSound;
    [ShowIf("playSound")]
    public string stringToPlay;
    public bool changement;
    [ShowIf("changement")]
    public List<GameObject> newObjets;
    [ShowIf("changement")]public List<GameObject> oldObjets;
    public bool isPetraTalking;
    public bool showingObject;
    public int nextDialogueIdx = 0;
    public enum DialogueType{Dialogue = 0, Didascalia = 1, Animation = 2, EndCinematic = 3}
}
