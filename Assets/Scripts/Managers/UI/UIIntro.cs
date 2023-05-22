using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIntro : LocalManager<UIIntro>
{
    [Header("Dialogue Box")]
    public TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBoxSprite;
    [SerializeField] private GameObject petraName;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject continueZone;
    [SerializeField] private GameObject shownedObjectSecion;

    [Space(5)]
    [Header("Dialogue Settings")]
    [SerializeField] private Color didascaliaColor;
    [SerializeField] private Color talkingColor;
    [SerializeField][Range(0, 1)] private float typingSpeed;

    [Space(5)]
    [Header("Fade")]
    [SerializeField] private CanvasGroup canvasToFade;
    [SerializeField] private float timeToFade;
    [SerializeField] private Image currentIllustration;
    [SerializeField] private Sprite newIllustration;
    bool hasFaded = false;

    public delegate void OnTextDisplayEnd();
    public OnTextDisplayEnd onDisplayEndDelegate;


    public void DisplayCinematicDialogue(CinematicLine dialogueLine)
    {
        StopCoroutine(UIIntro.Instance.Fade());
        CinematicTyper.Instance.StartTyping(dialogueLine, dialogueText, typingSpeed);
        HideContinueButton();

        onDisplayEndDelegate = () => DisplayContinueButton();
    }

    public IEnumerator Fade()
    {
        float timeElapsed = 0f;

        if(!hasFaded)
        {
            while (canvasToFade.alpha > 0)
            {
                canvasToFade.alpha = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            hasFaded = true;
        }
        else
        {
            while (canvasToFade.alpha < 1)
            {
                canvasToFade.alpha = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            hasFaded = false;
        }
        
        CinematicHandler.Instance.ContinueDialogue();
    }

    public void ChangeIllustration()
    {
        currentIllustration.sprite = newIllustration;
        StartCoroutine(AudioManager.Instance.Stop("InTrain_03"));
    }

    #region DialogueBox

    public void DisplayDialogueBox()
    {
        dialogueBoxSprite.SetActive(true);
    }

    public void HideDialogueBox()
    {
        dialogueBoxSprite.SetActive(false);
    }

    public void DisplayPetraNameSprite()
    {
        petraName.SetActive(true);
    }

    public void HidePetraNameSprite()
    {
        petraName.SetActive(false);
    }

    public void DisplayContinueButton()
    {
        continueButton.SetActive(true);
        if(CinematicHandler.Instance.cinematicDialogues.dialogueList[CinematicHandler.Instance.currentDialogueIdx].type == CinematicLine.DialogueType.Animation)
        {
            StartCoroutine(Fade());
        }
        else
        return;
    }

    public void HideContinueButton()
    {
        continueButton.SetActive(false);
    }

    public void DisplayContinueZone()
    {
        continueZone.SetActive(true);
    }

    public void HideContinueZone()
    {
        continueZone.SetActive(false);
    }

    public void DisplayShownedObject()
    {
        shownedObjectSecion.SetActive(true);
    }

    public void HideShownedObject()
    {
        shownedObjectSecion.SetActive(false);
    }

    #endregion

    #region DialogueText

    public void ResetTextValue()
    {
        dialogueText.text = "";
    }

    public void SetTalkingFont()
    {
        dialogueText.color = talkingColor;
        dialogueText.fontStyle = FontStyles.Normal;
    }

    public void SetDidascaliaFont()
    {
        dialogueText.color = didascaliaColor;
        dialogueText.fontStyle = FontStyles.Italic;
    }

    #endregion

}
