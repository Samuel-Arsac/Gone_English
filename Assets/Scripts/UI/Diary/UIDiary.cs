using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDiary : LocalManager<UIDiary>
{
    [Header("Sections")]
    [SerializeField] private GameObject parentSection;
    [SerializeField] private GameObject diarySection;
    [SerializeField] private GameObject proofsSection;
    [SerializeField] private GameObject profilesSection;

    [Space(5f)]
    [Header("Buttons")]
    [SerializeField] private Image diaryButton;
    [SerializeField] private GameObject bookIconClose;
    [SerializeField] private GameObject bookIconOpen;

    private GameObject lastSectionOpened;
    private Button lastSectionButton;
    private bool isDiaryMenuOpen;

    protected override void Awake()
    {
        base.Awake();
    }

    public void DisplayDiarySection()
    {        
        parentSection.SetActive(true);

        diarySection.SetActive(true);
        proofsSection.SetActive(false);
        profilesSection.SetActive(false);

        lastSectionOpened = diarySection;
        lastSectionButton = diaryButton.GetComponent<Button>();
        DisableButtonInteractions();
    }

    #region Parent Section
    public void DisplayParentSection()
    {
        if (UIManager.Instance.GetIntialisation())
        {
            AudioManager.Instance.PlaySFX("Book");
        }

        if (UIManager.Instance.currentMenuOpen != null && UIManager.Instance.currentMenuOpen.name != gameObject.name)
        {
            UIManager.Instance.CheckIfMenuIsOpen(gameObject);
        }

        if (isDiaryMenuOpen)
        {
            HideParentSection();
            if(UIManager.Instance.isInspectingEnviro)
            {
                UIManager.Instance.DisplayEnviroCursor();
            }
            
        }
        else
        {
            bookIconOpen.SetActive(true);
            bookIconClose.SetActive(false);
            isDiaryMenuOpen = true;
            DisplayDiarySection();

            UIManager.Instance.DisableInteractionEnvironnment();
            if (UIManager.Instance.isInspectingEnviro)
            {
                UIManager.Instance.HideEnviroCursor();
            }
            
            UIManager.Instance.menuAlreadyOpen = true;
            UIManager.Instance.currentMenuOpen = this.gameObject;
        }
    }

    public void HideParentSection()
    {
        bookIconOpen.SetActive(false);
        bookIconClose.SetActive(true);

        isDiaryMenuOpen = false;
        parentSection.SetActive(false);

        UIManager.Instance.EnableInteractionEnvironnment();

        EnableButtonInteractions();

        UIManager.Instance.menuAlreadyOpen = false;
        UIManager.Instance.currentMenuOpen = null;
    }

    #endregion

    #region Last Section
    public void DisplayLastSectionOpened(GameObject sectionToDisplay, Button clickedButton)
    {
        EnableButtonInteractions();
        sectionToDisplay.SetActive(true);
        HideLastSectionOpened();

        SetNewLastSection(sectionToDisplay, clickedButton);
    }

    public void HideLastSectionOpened()
    {
        lastSectionOpened.SetActive(false);
    }

    public void SetNewLastSection(GameObject newLastSection, Button newLastSectionButton)
    {
        lastSectionOpened = null;
        lastSectionOpened = newLastSection;
        lastSectionButton = newLastSectionButton;

        DisableButtonInteractions();
    }

    #endregion

    #region Button Interactions

    public void EnableButtonInteractions()
    {
        lastSectionButton.interactable = true;
    }

    public void DisableButtonInteractions()
    {
        lastSectionButton.interactable = false;
    }

    #endregion


}
