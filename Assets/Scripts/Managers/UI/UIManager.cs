using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using NaughtyAttributes;
public class UIManager : ProjectManager<UIManager>
{
    [SerializeField] private GameObject objectToShow;

    [Foldout("Menus")]
    public bool menuAlreadyOpen = false;
    [Foldout("Menus")]
    public GameObject currentMenuOpen = null;

    [Foldout("Environment Examen")]
    public GameObject presentInspeciton;
    [Foldout("Environment Examen")]
    public GameObject pastInspection;
    [Foldout("Environment Examen")]
    [SerializeField] private GameObject inspectionCursorToSpawn;
    [Foldout("Environment Examen")]
    [SerializeField] private GameObject inspectionCursorFindToSpawn;
    [Foldout("Environment Examen")]
    [SerializeField] private GameObject inspectionZone;
    [Foldout("Environment Examen")]
    [SerializeField] private Transform inspectionCusorToSpawnTransform;
    private GameObject inspectionCursor;
    private GameObject inspectionCursorFind;
    private bool canUseWatch = true;


    [SerializeField] GameObject watchButton;
    [SerializeField] GameObject watchButtonExamine;
    [SerializeField] GameObject watchButtonCantExamine;
    [SerializeField] private Sprite petraNameSpriteOriginal;
    [SerializeField] private GameObject transitionEnviroSection;
    [SerializeField] private GameObject examenEnviroBackButton;

    [SerializeField] private GameObject inventoryItem;
    [SerializeField] private GameObject worldMapItem;
    [SerializeField] private GameObject diaryItem;
    [SerializeField] private GameObject dailyItem;
    
    [HideInInspector] public bool isInspectingEnviro;
    [SerializeField] Transform originalInterlocutorPos;
    [SerializeField] Transform originalPetraNamePos;
    [SerializeField] Transform originalInterlocutorNamePos;
    [SerializeField] Transform originalPetraPos;

    [SerializeField] private GameObject quitConfirmSection;

    [SerializeField] private GameObject logSection;

    [Foldout("Fade Vignette")]
    [SerializeField] private float timeToFade = 1.25f;
    [Foldout("Fade Vignette")]
    [SerializeField] private float timeToFadeColor = 0.25f;
    [Foldout("Fade Vignette")]
    [SerializeField] private Color colorToFade;
    [Foldout("Fade Vignette")]
    [SerializeField] private Volume examenPostProcess;
    private Vignette _vignette;
    [Foldout("Fade Vignette")]
    bool fadeIn = true;
    public static bool firstInit = false;
    private bool isInitialized = false;

    #region Start

    protected override void Awake()
    {
        base.Awake();
        if(!firstInit)
        {
            SetPastInspection();
        }
        SetCanvasInteractable();
    }

    private void Start()
    {
        //SetCanvasInteractable();

        InitializeInstances();

        iconsCanvasGroup = icons.GetComponent<CanvasGroup>();

        SetPostProcess();

        EnableInteractionEnvironnment();
    }

    public void InitializeInstances()
    {
        inventoryItem.SetActive(true);
        inventoryItem.SetActive(false);

        worldMapItem.SetActive(true);
        worldMapItem.SetActive(false);

        diaryItem.SetActive(true);

        dailyItem.SetActive(true);
        dailyItem.SetActive(false);

        diaryItem.SetActive(false);
        isInitialized = true;

    }

    public bool GetIntialisation()
    {
        return isInitialized;
    }

    public void SetPastInspection()
    {
        firstInit = true;
        pastInspection = GameObject.Find("PastInspection");
    }

    public void HidePastInspection()
    {
        pastInspection.SetActive(false);
    }

    public void SetPostProcess()
    {
        pastInspection.transform.GetChild(1).GetComponent<Volume>().profile.TryGet<Vignette>(out _vignette);
        HidePastInspection();
    }

    public void SetCanvasInteractable()
    {
        interactableCanvas = worldCanvasGameObject.GetComponent<CanvasGroup>();
    }

    public void SetInspectionZone()
    {
        inspectionZone = pastInspection.transform.GetChild(0).gameObject;
    }

    public void GetSpawnCursorPosition()
    {
        inspectionCusorToSpawnTransform = inspectionZone.transform.GetChild(2);
    }

    #endregion

    #region Dialogue UI Variables
    [Foldout("Dialogue UI")]
    public GameObject dialogueCanvas;
    [Foldout("Dialogue UI")]
    public GameObject clickedCharacter;
    [Foldout("Dialogue UI")]
    public Image background;
    [Foldout("Dialogue UI")]
    public Image characterImage;
    [Foldout("Dialogue UI")]
    [SerializeField] private GameObject othersCharacters;
    [Foldout("Dialogue UI")]
    public Image otherCharacterSprite;
    [Foldout("Dialogue UI")]
    public Image petraImage;
    [Foldout("Dialogue UI")]
    public Image dialogueBox;
    [Foldout("Dialogue UI")]
    public TextMeshProUGUI dialogueBoxText;
    [Foldout("Dialogue UI")]
    public GameObject canvas;
    [Foldout("Dialogue UI")]
    public GameObject petraName;
    [Foldout("Dialogue UI")]
    public GameObject interlocutorName;
    [Foldout("Dialogue UI")]
    public Button continueButton;
    [Foldout("Dialogue UI")]
    [SerializeField] private GameObject continueZone;
    [Foldout("Dialogue UI")]
    [SerializeField] private Color normalDialogueColor;
    [Foldout("Dialogue UI")]
    [SerializeField] private Color monologueColor;
    [Foldout("Dialogue UI")]
    [SerializeField] private GameObject backColorNormal;
    [Foldout("Dialogue UI")]
    [SerializeField] private GameObject backColorPast;
    [Foldout("Dialogue UI")]
    [SerializeField] private Sprite unknownNameSprite;


    [Foldout("Dialogue Options")]
    public TextTyper textTyper;
    [Foldout("Dialogue Options")]
    private CharacterInfo _characterInfo;
    [Foldout("Dialogue Options")]
    [SerializeField] [Range(0, 1)] public float typingSpeed;
    [Foldout("Dialogue Options")]
    public DialogueHandler dialogueHandler;
    [Foldout("Feedbacks")]
    [SerializeField] private TextMeshProUGUI getItemText;
    [Foldout("Feedbacks")]
    [SerializeField] private GameObject feedbackExamen;
    
    

    [Foldout("UI")]
    public GameObject worldCanvasGameObject;
    private CanvasGroup interactableCanvas;
    [Foldout("UI")]
    [SerializeField] private GameObject icons;
      private CanvasGroup iconsCanvasGroup;
    [Foldout("UI")]
    [SerializeField] private Image inventoryButton;
    [Foldout("UI")]
    [SerializeField] private Image diaryButton;
    [Foldout("UI")]
    [SerializeField] private Image mapButton;
    [Foldout("UI")]
    [SerializeField] private GameObject blurEffect;

    //c'est une expression lambda c'est un principe un poil complexe qui mérite d'etre dig
    public delegate void OnTextDisplayEnd();

    public OnTextDisplayEnd onTextDisplayEnd;
    #endregion
  
    #region Dialogue Code
    //Cette fonction gère uniquement l'affichage du dialogue
    public void DisplayDialogueUI(DialogueLine dialogueLine)
    {
        if (dialogueLine.playDialogue)
        {
            Debug.Log(dialogueLine.dialogueToPlay);
            AudioManager.Instance.PlayDialogue(dialogueLine.dialogueToPlay);
        }

        if (dialogueLine.isFirstInterlocutorHere)
        {
            int spriteidx = dialogueLine.characterToTalkSpriteIdx;
            characterImage.sprite = _characterInfo.firstCharacterSprites[spriteidx];
        }
        

        int mcidx = dialogueLine.petraSpriteIdx;
        petraImage.sprite = _characterInfo.petraSprites[mcidx];

        if(_characterInfo.anotherInterlocutor)
        {
            int otheridx = dialogueLine.otherInterlocutorSpriteIdx;
            otherCharacterSprite.sprite = _characterInfo.otherToTalkSprites[otheridx];
        }

        DisplayContinueButton(false);
        EnableBlurEffect();
        textTyper.StartTyping(dialogueLine,dialogueBoxText,typingSpeed);

        if (dialogueLine.type == DialogueLine.DialogueType.Present)
        {
          onTextDisplayEnd = UIScrollingMenu.instance.DisplayPresentMenu;
          DisableContinueZone();
        }
        else
        {
          onTextDisplayEnd = () => DisplayContinueButton(true);
        }

        if(_characterInfo.isAlone)
        {
            SetMonologueTextTypography();
        }
        else
        {
            if(dialogueLine.isPetraThinking)
            {
                SetMonologueTextTypography();
            }
            else
            {
                SetNormalTextTypography();
            }            
        }


        if(!dialogueLine.isFirstInterlocutorHere && !dialogueLine.IsOtherInterlocutorHere && (dialogueLine.isPetraTalking || dialogueLine.isPetraThinking))
        {
            DialogueHandler.Instance.SetImageMonologuePosition(petraImage);
        }
        else
        {
            DialogueHandler.Instance.SetImageDialoguePosition(petraImage);
        }

        if(DialogueHandler.Instance.characterInfo.pastDialogue)
        {
            DisplayPastBackColor();
            DialogueHandler.Instance.SetImageDialoguePosition(characterImage);
            characterImage.material = null;

            DialogueHandler.Instance.SetToDefaultFirstPosition(otherCharacterSprite);
        }
        else
        {   
            HidePastBackColor();

            DialogueHandler.Instance.SetToDefaultFirstPosition(characterImage);
            DialogueHandler.Instance.SetToDefaultSecondPosition(otherCharacterSprite);
        }

        if(isInspectingEnviro)
        {
            examenEnviroBackButton.SetActive(false);
            HideEnviroCursor();
        }

        if(dialogueLine.showingObject)
        {
            DisplayObject();
            DialogueHandler.Instance.SetImageDialoguePosition(petraImage);
        }
        else
        {
            HideObject();
            if(dialogueLine.isFirstInterlocutorHere || dialogueLine.IsOtherInterlocutorHere)
            {
                DialogueHandler.Instance.SetImageDialoguePosition(petraImage);
            }
            else
            {
                DialogueHandler.Instance.SetImageMonologuePosition(petraImage);
            }
            
        }
    }

    public void DisplayPastBackColor()
    {
        backColorNormal.SetActive(false);
        backColorPast.SetActive(true);
    }

    public void HidePastBackColor()
    {
        backColorNormal.SetActive(true);
        backColorPast.SetActive(false);
    }

    public void DisplayNames()
    {
        DialogueLine currentLine = DialogueHandler.Instance.characterInfo.dialogueList[DialogueHandler.Instance.currentDialogueIdx];

        if (currentLine.isPetraTalking)
        {
            petraName.SetActive(true);
            interlocutorName.SetActive(false);
        }
        else
        {
            petraName.SetActive(false);
            interlocutorName.SetActive(true);
        }

        

        if(currentLine.noNames)
        {
            petraName.SetActive(false);
            interlocutorName.SetActive(false);
        }

        if(DialogueHandler.Instance.characterInfo.pastDialogue)
        {
            if (!currentLine.isPetraTalking && !currentLine.isThirdTalking)
            {
                interlocutorName.transform.position = originalPetraNamePos.position;

                if (currentLine.unknownCharacter)
                {
                    interlocutorName.GetComponent<Image>().sprite = unknownNameSprite;
                }
                else
                {
                    interlocutorName.GetComponent<Image>().sprite = DialogueHandler.Instance.characterInfo.characterNameSprite[0];
                }

                interlocutorName.GetComponent<Image>().SetNativeSize();
            }

            else if(currentLine.isThirdTalking && !currentLine.isPetraTalking)
            {

                interlocutorName.transform.position = originalInterlocutorNamePos.position;

                if (currentLine.unknownCharacter)
                {
                    interlocutorName.GetComponent<Image>().sprite = unknownNameSprite;
                }
                else
                {
                    interlocutorName.GetComponent<Image>().sprite = DialogueHandler.Instance.characterInfo.characterNameSprite[1];
                }
                interlocutorName.GetComponent<Image>().SetNativeSize();

            }
        }
        else
        {
            petraName.GetComponent<Image>().sprite = petraNameSpriteOriginal;
            if (currentLine.isThirdTalking)
            {
                interlocutorName.GetComponent<Image>().sprite = DialogueHandler.Instance.characterInfo.characterNameSprite[1];
            }
            else
            {
                interlocutorName.GetComponent<Image>().sprite = DialogueHandler.Instance.characterInfo.characterNameSprite[0];
            }

            interlocutorName.transform.position = originalInterlocutorNamePos.position;
        }

        if(currentLine.unknownCharacter)
        {
            interlocutorName.GetComponent<Image>().sprite = unknownNameSprite;
        }

    }

    public void DisplaySprites()
    {
        DialogueLine currentLine = DialogueHandler.Instance.characterInfo.dialogueList[DialogueHandler.Instance.currentDialogueIdx];

        if(currentLine.isPetraHere)
        {
            petraImage.gameObject.SetActive(true);
        }
        else
        {
            petraImage.gameObject.SetActive(false);
        }

        if(currentLine.isFirstInterlocutorHere)
        {
            characterImage.gameObject.SetActive(true);
        }
        else
        {
            characterImage.gameObject.SetActive(false);
        }

        if(currentLine.IsOtherInterlocutorHere)
        {
            othersCharacters.SetActive(true);
        }
        else
        {
            othersCharacters.SetActive(false);
        }

        if(characterImage.transform.position != originalInterlocutorPos.position)
        {
            if(characterImage.transform.rotation == originalInterlocutorPos.rotation)
            {
                characterImage.transform.Rotate(0f, 180f, 0f);
            }
            
        }
        else
        {
            if (characterImage.transform.rotation != originalInterlocutorPos.rotation)
            {
                characterImage.transform.Rotate(0f, -180f, 0f);
            }
           
        }
    }
    
    //c'est une fonction gérant l'affichage du bouton continuer, il récupére un type bool pour que depuis l'ondroit ou on l'appel il s'affiche ou non
    public void DisplayContinueButton(bool canContinue)
    {
      if(_characterInfo.dialogueList[DialogueHandler.Instance.currentDialogueIdx].changementAfterDialogue)
      {
          _characterInfo.dialogueList[DialogueHandler.Instance.currentDialogueIdx].objectsAfterChangement.SetActive(true);
      }
      continueButton.gameObject.SetActive(canContinue);
      EnableContinueZone();
    }

    public void EndDialogue(bool isAlone)
    {
        if(!isAlone)
        {
            CameraManager.instance.virtualCameraZoom.m_Priority -= 10;
            CameraManager.instance.virtualCameraZoom.m_Follow = null;
            DisableButtons();
            StartCoroutine(ResetCameraPosition());

            if(!DialogueHandler.Instance.characterInfo.autoLaunch && !DialogueHandler.Instance.characterInfo.isAlone)
            {
                clickedCharacter.gameObject.SetActive(true);
            }
        }
        else
        {
            EnableInteractionEnvironnment();
            EnableButtons();
        }
        if(characterInfo.gotLinkCharacter)
        {
            characterInfo.linkCharacter.SetActive(true);
        }

        if(characterInfo.newChangement)
      {
           if(characterInfo.newObjects != null)
           {
                foreach (GameObject g in characterInfo.newObjects)
                {
                    g.SetActive(true);
                }
           }
          
           if(characterInfo.destroyWhenChangement != null)
           {
                foreach (GameObject g in characterInfo.destroyWhenChangement)
                {
                    Destroy(g);
                }
           }

           if(characterInfo.objectsToInstantiate != null)
           {
                foreach(GameObject g in characterInfo.objectsToInstantiate)
                {
                    Instantiate(g, transform.position, transform.rotation);
                }
           }
      }

        if(characterInfo.pastDialogue)
        {
            DisableInteractionEnvironnment();
            AudioManager.Instance.SwapMusic(ScenesManager.Instance.GetSceneName());
        }

        DisableBlurEffect();
        DisplayIcons();
        
        dialogueCanvas.SetActive(false);
    }
    
    IEnumerator ResetCameraPosition()
    {
        yield return new WaitForSeconds(CameraManager.instance.cinemachineBrain.m_DefaultBlend.m_Time);

        if (isInspectingEnviro)
        {
            DisplayEnviroCursor();
            EnableEnvironementExamen();
        }

        EnableInteractionEnvironnment();
        EnableButtons();

    }
    
    //getter et setter du character info cet élèments est ce qui est censée etre donné a l'UImanager/handler pour savoir qui parle et les info de qui parle
    public CharacterInfo characterInfo
    {
      get => _characterInfo;
      set
      {
        _characterInfo = value;
        characterImage.sprite = _characterInfo.firstCharacterSprites[0];
        petraImage.sprite = characterInfo.petraSprites[0];
      }
    }
    #endregion

    #region Feedbacks
    public void DisplayGetItemText(string objectName)
    {
        getItemText.text = objectName + " est maintenant dans l'inventaire";
        getItemText.gameObject.SetActive(true);
    }

    public void DisplayFeedback()
    {
        feedbackExamen.SetActive(true);
    }

    public void HideFeedback()
    {
        feedbackExamen.SetActive(false);
    }

    public void DisplayFailedExaminedText(string objectName)
    {
        getItemText.text = "Il n'y a rien à analyser sur " + objectName;
        getItemText.gameObject.SetActive(true);
    }

    public void DisplayIntrestPointText()
    {
      getItemText.text = "Point d'intérêt détecté !";
      getItemText.gameObject.SetActive(true);
    }

    public void HideIntrestPointText()
    {
      getItemText.text = null;
      getItemText.gameObject.SetActive(false);
    }

    public void EnableBlurEffect()
    {
        blurEffect.SetActive(true);
    }

    public void DisableBlurEffect()
    {
        blurEffect.SetActive(false);
    }
    #endregion

    #region Others

    public void DisplayIcons()
    {
        icons.SetActive(true);
    }

    public void HideIcons()
    {
        icons.SetActive(false);
    }

    public void DisableInventoryButtonInteraction()
    {
        inventoryButton.raycastTarget = false;
    }
    public void EnableInventoryButtonInteraction()
    {
        inventoryButton.raycastTarget = true;
    }
    public void DisableDiaryButtonInteraction()
    {
        diaryButton.raycastTarget = false;
    }
    public void EnableDiaryButtonInteraction()
    {
        diaryButton.raycastTarget = true;
    }
    public void DisableMapButtonInteraction()
    {
        mapButton.raycastTarget = false;
    }
    public void EnableMapButtonInteraction()
    {
        mapButton.raycastTarget = true;
    }

    public void DisableInteractionEnvironnment()
    {
        //Debug.Log("DisableInteractionEnvironnement");
        interactableCanvas.interactable = false;
        interactableCanvas.blocksRaycasts = false;
    }

    public void EnableInteractionEnvironnment()
    {
        interactableCanvas.interactable = true;
        interactableCanvas.blocksRaycasts = true;
    }


    public void DisableButtons()
    {
        iconsCanvasGroup.interactable = false;
        iconsCanvasGroup.blocksRaycasts = false;
    }

    public void EnableButtons()
    {
        iconsCanvasGroup.interactable = true;
        iconsCanvasGroup.blocksRaycasts = true;
    }

    public void SetMonologueTextTypography()
    {
        dialogueBoxText.fontStyle = FontStyles.Italic;
        dialogueBoxText.color = monologueColor;
    }

    public void SetNormalTextTypography()
    {
        dialogueBoxText.fontStyle = FontStyles.Normal;
        dialogueBoxText.color = normalDialogueColor;
    }

    public void EnableContinueZone()
    {
        continueZone.SetActive(true);
    }

    public void DisableContinueZone()
    {
        continueZone.SetActive(false);
    }

    public void CheckIfMenuIsOpen(GameObject menuOpen)
    {
        if(menuAlreadyOpen)
        {
            switch (currentMenuOpen.name)
            {
                case "//Inventory":
                    UIInventory.Instance.HideUIInventory();
                    break;
                case "//Map Section":
                    UIWorldMap.Instance.DisplayUIWordlMap();
                    break;
                case "//Diary Section":
                    UIDiary.Instance.HideParentSection();
                    break;
            }
            currentMenuOpen = menuOpen;
        }
    }
    #endregion

    #region Examen Environement
    public void EnableEnvironementExamen()
    {
        if(canUseWatch)
        {
            GetSpawnCursorPosition();
            if (isInspectingEnviro)
            {
                CallFade();
                examenEnviroBackButton.SetActive(false);
                pastInspection.SetActive(false);

                isInspectingEnviro = false;
                CursorsManager.instance.DisplayCursor();


                if (inspectionCursor == null)
                {
                    return;
                }
                else
                {
                    inspectionCursor.SetActive(false);
                    inspectionCursorFind.SetActive(false);
                }
                DisplayIcons();

                presentInspeciton.SetActive(true);

                if(!DialogueHandler.Instance.characterInfo.pastDialogue)
                {
                    AudioManager.Instance.SwapMusic(ScenesManager.Instance.GetSceneName());
                }
            }
            else
            {
                
                CallFade();

                if(ScenesManager.Instance == null)
                {
                    Debug.Log("ça marche pas");
                }
                else
                {
                    AudioManager.Instance.SwapMusic(ScenesManager.Instance.GetSceneName() + "_Reverse");
                }

                CursorsManager.instance.HideCursor();
                EnableInteractionEnvironnment();
                HideIcons();
                isInspectingEnviro = true;
                pastInspection.SetActive(true);
                presentInspeciton.SetActive(false);
                examenEnviroBackButton.SetActive(true);

                if (inspectionCursor == null)
                {
                    inspectionCursor = Instantiate(inspectionCursorToSpawn, inspectionCusorToSpawnTransform);
                    inspectionCursorFind = Instantiate(inspectionCursorFindToSpawn, inspectionCusorToSpawnTransform);
                    inspectionCursor.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    inspectionCursorFind.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    inspectionCursorFind.SetActive(false);
                }
                else
                {
                    inspectionCursor.SetActive(true);
                }
            }
        }
        else
        {
            DialogueHandler.Instance.CantExamineDialogue();
        }
        

    }

    public void CantUseWatch()
    {
        canUseWatch = false;
        watchButtonCantExamine.SetActive(true);
        watchButtonExamine.SetActive(false);

    }

    public void DisplayTransition()
    {
        transitionEnviroSection.SetActive(true);
    }

    public void HideTransition()
    {
        transitionEnviroSection.SetActive(false);
    }

    public void DisplayEnviroCursor()
    {
        inspectionCursor.SetActive(true);
        CursorsManager.instance.HideCursor();
    }

    public void HideEnviroCursor()
    {
        inspectionCursor.SetActive(false);
        CursorsManager.instance.DisplayCursor();
    }

    public void DisplayWatchButton()
    {
        watchButton.SetActive(true);
    }

    public void HideWatchButton()
    {
        watchButton.SetActive(false);
    }

    public void CallFade()
    {
        StartFade(0f, _vignette, Color.white);
    }

    public void StartFade(float intensity, Vignette vignette, Color color)
    {
        StartCoroutine(fadeVignette(intensity, vignette, color));
    }

    IEnumerator fadeVignette(float mainIntensity, Vignette fadedVignette, Color mainColor)
    {
        float timeElapsed = 0f;
        if(fadeIn)
        {
            while (timeElapsed < timeToFade)
            {
                fadedVignette.color.value = Color.Lerp(mainColor, colorToFade, timeElapsed / timeToFadeColor);
                fadedVignette.intensity.value = Mathf.Lerp(mainIntensity, 1, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (timeElapsed < timeToFade)
            {
                fadedVignette.color.value = Color.Lerp(colorToFade, mainColor, timeElapsed / timeToFadeColor);
                fadedVignette.intensity.value = Mathf.Lerp( 1, mainIntensity, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        
        fadeIn = !fadeIn;

    }



    #endregion

    public void DisplayObject()
    {
        objectToShow.SetActive(true);
    }

    public void HideObject()
    {
        objectToShow.SetActive(false);
    }

    private void Update()
    {
        if (!isInspectingEnviro)
        {
            return;
        }
        else
        {
            inspectionCursor.transform.position = InputsManager.Instance.ReadMousePositionValueWorldSpace();
            inspectionCursorFind.transform.position = InputsManager.Instance.ReadMousePositionValueWorldSpace();
        }
    }

    public RectTransform GetInspectorCursorPosition()
    {
        return inspectionCursor.transform as RectTransform;
    }

    public GameObject GetInspectionCursorFind()
    {
        return inspectionCursorFind;
    }

    public void DisplayQuitConfirm()
    {
        quitConfirmSection.SetActive(true);
        DisableInteractionEnvironnment();
        Time.timeScale = 0f;

        InputsManager.Instance.controls.Keyboard.Back.performed -= InputsManager.Instance.EscapeClick;
        InputsManager.Instance.controls.Keyboard.Back.performed += InputsManager.Instance.EscapeLeave;
    }

    public void HideQuitConfirm()
    {
        quitConfirmSection.SetActive(false);
        EnableInteractionEnvironnment();
        Time.timeScale = 1f;

        InputsManager.Instance.controls.Keyboard.Back.performed += InputsManager.Instance.EscapeClick;
        InputsManager.Instance.controls.Keyboard.Back.performed -= InputsManager.Instance.EscapeLeave;
    }

    public void DisplayLogSection()
    {
        logSection.SetActive(true);
        InputsManager.Instance.controls.Keyboard.Tab.performed += InputsManager.Instance.LogLeave;
        InputsManager.Instance.controls.Keyboard.Tab.performed -= InputsManager.Instance.LogClick;
    }

    public void HideLogSection()
    {
        logSection.SetActive(false);
        InputsManager.Instance.controls.Keyboard.Tab.performed -= InputsManager.Instance.LogLeave;
        InputsManager.Instance.controls.Keyboard.Tab.performed += InputsManager.Instance.LogClick;
    }
}
