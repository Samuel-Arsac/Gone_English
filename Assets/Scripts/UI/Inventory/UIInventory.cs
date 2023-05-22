using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIInventory : LocalManager<UIInventory>
{
    public bool gotWatch = false;
    [SerializeField] private GameObject openBagButton;
    [SerializeField] private GameObject closeBagButton;
    [SerializeField] private List<Transform> slotsList;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject inventorySection;
    [SerializeField] private GameObject descriptionSection;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemSubtitleText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI postDescriptionText;
    [SerializeField] private GameObject examenSection;
    [SerializeField] private GameObject transitionSection;
    [SerializeField] private GameObject inventoryWatchUI;
    [SerializeField] private GameObject animatedWatch;
    
    private Item readItem;

    [HideInInspector] public Item itemExamined;
    private bool isInventoryOpen;
    [SerializeField] private LayoutGroup grid = null;

    private EventTrigger trigger;
    private EventTrigger.Entry entryClick;
    private EventTrigger.Entry entryEnter;
    private EventTrigger.Entry entryExit;

    EventTrigger.Entry hoverEntry;
    EventTrigger.Entry exitEntry;
    private Highlight highlight;

    protected override void Awake()
    {
        base.Awake();

        grid.CalculateLayoutInputHorizontal();
        grid.SetLayoutHorizontal();
        grid.CalculateLayoutInputVertical();
        grid.SetLayoutVertical();
        highlight = inventoryWatchUI.GetComponent<Highlight>();
    }

    private void OnEnable() 
    {
        UIManager.Instance.DisableInteractionEnvironnment();
        List<Item> items = null;

        items = ItemsManager.Instance.GetInventory();

        if (gotWatch)
        {
            inventoryWatchUI.SetActive(true);
        }
        else
        {
            inventoryWatchUI.SetActive(false);
            animatedWatch.SetActive(false);
        }

        for(int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = slotsList[i].position;
            items[i].transform.localScale = slotsList[i].localScale;
            items[i].gameObject.SetActive(true);

            EventTrigger trigger = items[i].gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = trigger.triggers.Find(t => t.eventID == EventTriggerType.PointerClick);
            entry.callback.AddListener(DisplayDescriptionOnClick);

            Items_SO itemData = items[i].GetComponent<Item>().itemData;

            if(gotWatch)
            {
                if (itemData.canExamine)
                {
                    hoverEntry = trigger.triggers.Find(t => t.eventID == EventTriggerType.PointerEnter);
                    hoverEntry.callback.AddListener(DisplayWatchAnimatedOnHover);

                    exitEntry = trigger.triggers.Find(t => t.eventID == EventTriggerType.PointerExit);
                    exitEntry.callback.AddListener(HideWatchAnimatedOnExit);
                }
            }
        }
    }

    private void OnDisable()
    {
        List<Item> items = null;
        items = ItemsManager.Instance.GetInventory();


        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = Vector3.zero;
            items[i].gameObject.SetActive(false);
            EventTrigger trigger = items[i].gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = trigger.triggers.Find(t => t.eventID == EventTriggerType.PointerClick);
            entry.callback.RemoveListener(DisplayDescriptionOnClick);
        }
    }

    

    #region Inventory
    public void DisplayInventory()
    {
        if (UIManager.Instance.GetIntialisation())
        {
            AudioManager.Instance.PlaySFX("Bag");
        }

        if (UIManager.Instance.currentMenuOpen != null && UIManager.Instance.currentMenuOpen.name != gameObject.name)
        {
            UIManager.Instance.CheckIfMenuIsOpen(gameObject);
        }
        
        if (isInventoryOpen)
        {
            UIManager.Instance.EnableInteractionEnvironnment();
            if(UIManager.Instance.isInspectingEnviro)
            {
                UIManager.Instance.DisplayEnviroCursor();
            }
            
            
            HideUIInventory();
           
            UIManager.Instance.menuAlreadyOpen = false;
            UIManager.Instance.currentMenuOpen = null;
        }
        else
        {
            UIManager.Instance.DisableInteractionEnvironnment();
            if(UIManager.Instance.isInspectingEnviro)
            {
                UIManager.Instance.HideEnviroCursor();
            }
            
            closeBagButton.SetActive(false);
            openBagButton.SetActive(true);

            inventory.SetActive(true);
            if(gotWatch)
            {
                inventoryWatchUI.SetActive(true);
            }
            examenSection.SetActive(false);
            isInventoryOpen = true;

            UIManager.Instance.menuAlreadyOpen = true;
            UIManager.Instance.currentMenuOpen = this.gameObject;
        }                     
    }

    public void DisplayWatchAnimatedOnHover(BaseEventData ctx)
    {
        animatedWatch.SetActive(true);
    }

    public void HideWatchAnimatedOnExit(BaseEventData ctx)
    {
        animatedWatch.SetActive(false);
    }

    public void HideUIInventory()
    {
        closeBagButton.SetActive(true);
        openBagButton.SetActive(false);

        inventory.SetActive(false); 
        isInventoryOpen = false;
        HideDescription();
    }

    public void UnlockWatch()
    {
        gotWatch = true;
    }

    #endregion

    #region Description

    public void DisplayDescriptionOnClick(BaseEventData ctx)
    {
        AudioManager.Instance.PlaySFX("ClickTik");
        PointerEventData data = ctx as PointerEventData;
        Item clickedItem = data.pointerPress.GetComponent<Item>();
        if (readItem == null)
        {
            DisplayDescription(clickedItem);
        }
        else
        {
            if (readItem != clickedItem)
            {
                DisplayDescription(clickedItem);
            }
            else
            {
                HideDescription();
            }
        }
    }

    public void DisplayDescription(Item itemClicked)
    {
        if(gotWatch)
        {
            exitEntry.callback.RemoveListener(HideWatchAnimatedOnExit);
        }
        
        readItem = itemClicked;
        descriptionSection.SetActive(true);
        itemNameText.text = itemClicked.itemData.itemName;
        itemSubtitleText.text = itemClicked.itemData.itemSubtitle;
        itemDescriptionText.text = itemClicked.itemData.itemDescription;

        if(itemClicked.itemData.isExamined)
        {
            postDescriptionText.gameObject.SetActive(true);
            postDescriptionText.text = itemClicked.itemData.descriptionPostExamen;
        }
        else
        {
            postDescriptionText.gameObject.SetActive(false);
        }

        if(itemClicked.itemData.canExamine && gotWatch)
        {
            AddFunctionsToTrigger();
            animatedWatch.SetActive(true);
        }
        else
        {
            RemoveFunctionToTrigger();
            animatedWatch.SetActive(false);
        }
    }

    public void CallHideDescription(BaseEventData data)
    {
        HideDescription();
    }

    public void HideDescription()
    {
        if(gotWatch)
        {
            exitEntry.callback.AddListener(HideWatchAnimatedOnExit);
        }
        
        RemoveFunctionToTrigger();
        descriptionSection.SetActive(false);
        animatedWatch.SetActive(false);
        itemNameText.text = null;
        itemSubtitleText.text = null;
        itemDescriptionText.text = null;
        readItem = null;
    }

    #endregion


    #region Examen

    public void AddFunctionsToTrigger()
    {
        trigger = inventoryWatchUI.AddComponent<EventTrigger>();

        //Event lors du click
        entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener(DisplayExamenUI);
        entryClick.callback.AddListener(highlight.OnExit);
        trigger.triggers.Add(entryClick);

        //Event lors du hover de la souris
        entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener(highlight.OnHover);
        trigger.triggers.Add(entryEnter);

        //Event lors de l'exit de la souris
        entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener(highlight.OnExit);
        trigger.triggers.Add(entryExit);
    }

    public void RemoveFunctionToTrigger()
    {
        Destroy(trigger);
    }

    public void DisplayExamenUI(BaseEventData ctx)
    {
        PointerEventData data = ctx as PointerEventData;
        itemExamined = readItem;
        DisplayTransition();
        AudioManager.Instance.SwapMusic(ScenesManager.Instance.GetSceneName() + "_Reverse");
        DisplayInventory();
    }

    public void DisplayTransition()
    {
        transitionSection.SetActive(true);
    }

    public void HideTransition()
    {
        transitionSection.SetActive(false);
        examenSection.SetActive(true);
        RemoveFunctionToTrigger();
    }

    public void EndExamen()
    {
        itemExamined.itemData.isExamined = true;
        itemExamined = null;
    }

    public void HideInvetory()
    {
        inventorySection.SetActive(false);
        examenSection.SetActive(true);
        isInventoryOpen = false;
    }

    #endregion
}
