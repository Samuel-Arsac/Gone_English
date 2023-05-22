using UnityEngine;
using TMPro;

public class ExamenManager : LocalManager<ExamenManager>
{
    [SerializeField] private GameObject examenSection;
    [SerializeField] private Transform spawnSpritePosition;
    [SerializeField] private GameObject informationsSection;
    [SerializeField] private TextMeshProUGUI informationsText;
    private GameObject itemPastSprite;

    private void Start()
    {
        UIManager.Instance.DisableInteractionEnvironnment();
    }
    private void OnEnable() 
    {
        if(itemPastSprite == null /*|| itemPastSprite.name != UIInventory.Instance.itemExamined.name*/)
        {
            itemPastSprite = Instantiate(UIInventory.Instance.itemExamined.itemData.pastItem, spawnSpritePosition);
        }
        else
        {
            itemPastSprite.SetActive(true);
        }
        
        informationsSection.SetActive(false);
        informationsText.text = null;
    }

    private void OnDisable() 
    {
        AudioManager.Instance.SwapMusic("Hangars");
        itemPastSprite.SetActive(false);
    }
    
    public void OnHoverIntrestPoint()
    {
        UIManager.Instance.DisplayIntrestPointText();
    }

    public void OnExitIntrestPoint()
    {
        UIManager.Instance.HideIntrestPointText();
    }

    public void ClickMajorPoint(CharacterInfo dialogueToPlay)
    {
        UIManager.Instance.DisplayFeedback();
        UIInventory.Instance.itemExamined.itemData.isExamined = true;
        examenSection.SetActive(false);
        OnExitIntrestPoint();
        CursorsManager.instance.DisplayCursor();
        DialogueHandler.Instance.startDialogue(dialogueToPlay, dialogueToPlay.isAlone);
        UIInventory.Instance.EndExamen();
    }

    public void ClickMinorPoint(IntrestPoint clickedInformations)
    {
        informationsSection.SetActive(true);
        informationsText.text = clickedInformations.pointInformations;
    }

    public void QuitExamen()
    {
        examenSection.SetActive(false);
        UIManager.Instance.EnableInteractionEnvironnment();
        UIManager.Instance.DisplayIcons();
    }

}
