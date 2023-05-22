using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : ProjectManager<DialogueHandler>
{
    private string soundtostring;

    [HideInInspector] public CharacterInfo characterInfo;
    [SerializeField] private CharacterInfo cantExamineDialogue;
    [SerializeField] private CharacterInfo afterLetterExamenDialogue;

    [SerializeField] private List<CharacterInfo> cantTravelDialogues;
    public int  currentDialogueIdx;
    [SerializeField] private Transform monologuePostion;
    [SerializeField] private Transform dialoguePostion;

    [SerializeField] private Transform defaultFirstInterlocutorPostion;
    [SerializeField] private Transform defaultSecondInterlocutorPosition;
    [SerializeField] private TextTyper textTyper;
    [SerializeField] [Range(0, 20)] private float typingSpeed;

    private void Start() 
    {
        StopAllCoroutines();
    }

    public void OnDialogueLineEnd()
   {
       List<DialogueLine> dialogueList = UIManager.Instance.characterInfo.dialogueList;
       
   }

   //au moment ou on lance le dialogue avec le minion
   public void startDialogue(CharacterInfo characterInfo, bool isAlone)
   {
        if(UIManager.Instance.currentMenuOpen != null)
        {
            UIManager.Instance.CheckIfMenuIsOpen(UIManager.Instance.currentMenuOpen);
        }

        if(UIManager.Instance.isInspectingEnviro)
        {
            UIManager.Instance.EnableEnvironementExamen();
        }

        this.characterInfo = characterInfo;
        CursorsManager.instance.ChangeCursorTexture(0);
        currentDialogueIdx = 0;
       
        if(!characterInfo.autoLaunch && !isAlone)
        {
            UIManager.Instance.clickedCharacter.SetActive(false);
            if(characterInfo.gotLinkCharacter)
            {
                characterInfo.linkCharacter.SetActive(false);
            }
        }
        else
        {
            UIManager.Instance.clickedCharacter = null;
        }
    
        if(isAlone)
        {
            UIManager.Instance.characterImage.gameObject.SetActive(false);
            SetImageMonologuePosition(UIManager.Instance.petraImage);
        }
    
        else
        {
            UIManager.Instance.interlocutorName.GetComponent<Image>().sprite = characterInfo.characterNameSprite[0];
            UIManager.Instance.characterImage.gameObject.SetActive(true);
            SetImageDialoguePosition(UIManager.Instance.petraImage);
            UIManager.Instance.petraImage.sprite = characterInfo.firstCharacterSprites[currentDialogueIdx];
        }

        UIManager.Instance.DisplaySprites();
        UIManager.Instance.DisplayNames();
        FirstDialogueUIDisplay();

   }

    public void CantExamineDialogue()
    {
        startDialogue(cantExamineDialogue, true);
    }

   public void FirstDialogueUIDisplay()
    {
        UIManager.Instance.dialogueCanvas.SetActive(true);
        UIManager.Instance.dialogueBoxText.text = "";
        
        currentDialogueIdx = 0;
        UIManager.Instance.canvas.SetActive(true);
        UIManager.Instance.characterInfo = characterInfo;
        UIManager.Instance.DisplayDialogueUI(characterInfo.dialogueList[currentDialogueIdx]);
        UIManager.Instance.DisableInteractionEnvironnment();
        UIManager.Instance.DisableButtons();
        UIManager.Instance.HideIcons();
    }
   
   //gére le comportement du bouton continue, est a appeler au onclick du bouton continuer
   public void ContinueDialogue()
   {
       CursorsManager.instance.ChangeCursorTexture(0);
       switch (characterInfo.dialogueList[currentDialogueIdx].type)
       {
           case DialogueLine.DialogueType.Dialogue:
               currentDialogueIdx = characterInfo.dialogueList[currentDialogueIdx].nextDialogueIdx;
               UIManager.Instance.DisplayDialogueUI(characterInfo.dialogueList[currentDialogueIdx]);
                break;
            case DialogueLine.DialogueType.Present:
                currentDialogueIdx = characterInfo.dialogueList[currentDialogueIdx].nextDialogueIdx;
                UIManager.Instance.DisplayDialogueUI(characterInfo.dialogueList[currentDialogueIdx]);
                UIScrollingMenu.instance.HidePresentMenu();
                break;
            case DialogueLine.DialogueType.EndDialogue:
                UIManager.Instance.EndDialogue(false);
                break;
            case DialogueLine.DialogueType.EndMonologue:
                UIManager.Instance.EndDialogue(true);
                break;
           default:
               break;
       }

       UIManager.Instance.DisplaySprites();
       UIManager.Instance.DisplayNames();

        if (characterInfo.dialogueList[currentDialogueIdx].playSound)
        {
            AudioManager.Instance.PlaySFX(characterInfo.dialogueList[currentDialogueIdx].soundToPlay);
        }
   }

   public void GoodProofPresentation()
   {
        CursorsManager.instance.DisplayDefaultCursor();
       currentDialogueIdx = characterInfo.presentLists[0].dialogueToGo;
       UIManager.Instance.DisplayDialogueUI(characterInfo.dialogueList[currentDialogueIdx]);
       UIManager.Instance.DisplaySprites();
       UIManager.Instance.DisplayNames();
       UIScrollingMenu.instance.HidePresentMenu();
   }

    public void ContinueButton()
    {
        if (textTyper.isTyping)
        {
            TextTyper.Instance.EndTyping(characterInfo.dialogueList[currentDialogueIdx], UIManager.Instance.dialogueBoxText);
            UIManager.Instance.dialogueBoxText.text = characterInfo.dialogueList[currentDialogueIdx].text;
            UIManager.Instance.onTextDisplayEnd();

        }
        else
        {
            ContinueDialogue();
        }
    }

    public void LeaveDialogue(int indexLeave)
   {
       currentDialogueIdx = indexLeave;
       UIManager.Instance.DisplayDialogueUI(characterInfo.dialogueList[currentDialogueIdx]);
       UIManager.Instance.DisplaySprites();
       UIManager.Instance.DisplayNames();
       UIScrollingMenu.instance.HidePresentMenu();
   }

    public void StartDialogueCantTravelRailway()
    {
        startDialogue(cantTravelDialogues[0], true);
    }

    public void StartDialogueCantTravelAway()
    {
        startDialogue(cantTravelDialogues[1], true);
    }

    public void StartDialogueCantTravel()
    {
        startDialogue(cantTravelDialogues[2], true);
    }

    public void StartDialogueCantTravelWatch()
    {
        startDialogue(cantTravelDialogues[3], true);
    }

   public void SetImageDialoguePosition(Image imageToMove)
   {
        imageToMove.transform.position = dialoguePostion.position;
        imageToMove.transform.localScale = dialoguePostion.localScale;
   }


    public void SetImageMonologuePosition(Image imageToMove)
    {
        imageToMove.transform.position = monologuePostion.position;
        imageToMove.transform.localScale = monologuePostion.localScale;
    }

    public void SetToDefaultFirstPosition(Image imageToMove)
    {
        imageToMove.transform.position = defaultFirstInterlocutorPostion.position;
        imageToMove.transform.localScale = defaultFirstInterlocutorPostion.localScale;
    }

    public void SetToDefaultSecondPosition(Image imageToMove)
    {
        imageToMove.transform.position = defaultSecondInterlocutorPosition.position;
        imageToMove.transform.localScale = defaultSecondInterlocutorPosition.localScale;

    }

    public void StartDialogueAfterExamen()
    {
        StartCoroutine(afterLetterExamenDialogue.waitForDialogueToStart());
    }

}
