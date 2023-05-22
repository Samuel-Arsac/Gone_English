using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CinematicDialogue : MonoBehaviour
{
    [SerializeField] private List<CinematicLine> myDialogueList  = new List<CinematicLine>();
    
    public List<CinematicLine> dialogueList
    {
        get { return myDialogueList; }
        private set { myDialogueList = value; }
    }
}
