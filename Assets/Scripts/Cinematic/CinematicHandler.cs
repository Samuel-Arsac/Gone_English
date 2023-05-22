using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicHandler : LocalManager<CinematicHandler>
{
    public CinematicDialogue cinematicDialogues;
    public bool autoLaunch;
    public float timeToWait;
    public int  currentDialogueIdx;
    [SerializeField] private CinematicTyper textTyper;
   [SerializeField] [Range(0, 20)] private float typingSpeed;

    private void Start() 
    {
        cinematicDialogues = GetComponent<CinematicDialogue>();
    }

    public void OnDialogueLineEnd()
   {
       List<CinematicLine> dialogueList = cinematicDialogues.dialogueList;
       
   }

   //au moment ou on lance le dialogue avec le minion
   public void startDialogue(CinematicDialogue cinematicDialogue)
   {
        CursorsManager.instance.ChangeCursorTexture(0);
        currentDialogueIdx = 0;
        DisplayDialogue();
   }

   public void DisplayDialogue()
    {
        UIIntro.Instance.DisplayDialogueBox();
        UIIntro.Instance.ResetTextValue();

        DisplayNames(cinematicDialogues.dialogueList[currentDialogueIdx].isPetraTalking);
        
        currentDialogueIdx = 0;
        UIIntro.Instance.DisplayCinematicDialogue(cinematicDialogues.dialogueList[currentDialogueIdx]);
    }
   
   //gére le comportement du bouton continue, est a appeler au onclick du bouton continuer
   public void ContinueDialogue()
   {
       CursorsManager.instance.ChangeCursorTexture(0);
       currentDialogueIdx = cinematicDialogues.dialogueList[currentDialogueIdx].nextDialogueIdx;
       switch (cinematicDialogues.dialogueList[currentDialogueIdx].type)
       {
            case CinematicLine.DialogueType.Dialogue:
                UIIntro.Instance.DisplayContinueZone();
                UIIntro.Instance.SetTalkingFont();
                break;
            case CinematicLine.DialogueType.Didascalia:
                UIIntro.Instance.DisplayContinueZone();
                UIIntro.Instance.SetDidascaliaFont();
                break;
            case CinematicLine.DialogueType.Animation:
                UIIntro.Instance.HideContinueZone();
                break;
            case CinematicLine.DialogueType.EndCinematic:
                LoadGame();
                break;
       }

       if(cinematicDialogues.dialogueList[currentDialogueIdx].showingObject)
       {
           UIIntro.Instance.DisplayShownedObject();
       }
       else
       {
           UIIntro.Instance.HideShownedObject();
       }

       if(cinematicDialogues.dialogueList[currentDialogueIdx].playSound)
       {
           AudioManager.Instance.PlaySFX(cinematicDialogues.dialogueList[currentDialogueIdx].stringToPlay);
       }

       if(cinematicDialogues.dialogueList[currentDialogueIdx].changeIllustration)
       {
           UIIntro.Instance.ChangeIllustration();
       }

        if (cinematicDialogues.dialogueList[currentDialogueIdx].changement)
        {
            if(cinematicDialogues.dialogueList[currentDialogueIdx].newObjets != null)
            {
                foreach (GameObject g in cinematicDialogues.dialogueList[currentDialogueIdx].newObjets)
                {
                    g.SetActive(true);
                }
            }

            if (cinematicDialogues.dialogueList[currentDialogueIdx].oldObjets != null)
            {
                foreach (GameObject g in cinematicDialogues.dialogueList[currentDialogueIdx].oldObjets)
                {
                    g.SetActive(false);
                }
            }
            
        }
       
       UIIntro.Instance.DisplayCinematicDialogue(cinematicDialogues.dialogueList[currentDialogueIdx]);
       DisplayNames(cinematicDialogues.dialogueList[currentDialogueIdx].isPetraTalking);

       
   }

    public void ContinueButton()
    {
        if(textTyper.isTyping)
        {
            CinematicTyper.Instance.EndTyping(cinematicDialogues.dialogueList[currentDialogueIdx], UIIntro.Instance.dialogueText);
            UIIntro.Instance.dialogueText.text = cinematicDialogues.dialogueList[currentDialogueIdx].text;
            UIIntro.Instance.onDisplayEndDelegate();
        }
        else
        {
            ContinueDialogue();
        }
    }

   public void DisplayNames(bool isPetraTalking)
   {
       if(isPetraTalking)
       {
           UIIntro.Instance.DisplayPetraNameSprite();
       }
       else
       {
           UIIntro.Instance.HidePetraNameSprite();
       }
   }
 
    public void LoadGame()
    {
        SceneManager.LoadScene("Gare");
    }
}
