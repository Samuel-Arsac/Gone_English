using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIEvidence : MonoBehaviour
{
    [SerializeField] private GameObject proofsInformations;
    [SerializeField] private TextMeshProUGUI proofsName;

    [SerializeField] private List<Transform> slotsList;
    [SerializeField] private RectTransform slotsParents;
    [SerializeField] private VerticalLayoutGroup grid;
    private EventTrigger trigger;
    private List<EventTrigger.Entry> entryEnter;
    private List<EventTrigger.Entry> entryExit;

    private void Awake()
    {
        grid.CalculateLayoutInputHorizontal();
        grid.SetLayoutHorizontal();
        grid.CalculateLayoutInputVertical();
        grid.SetLayoutVertical();
    }

    private void OnEnable() 
    {
        List<Item> items = null;
        items = ItemsManager.Instance.GetExaminedObjects();

        entryEnter = new List<EventTrigger.Entry>();
        entryExit = new List<EventTrigger.Entry>();

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = slotsList[i].position;
            items[i].transform.localScale = slotsList[i].localScale;
            
            Draggable drag = items[i].gameObject.AddComponent<Draggable>();
            drag.itemCanvasGroup = items[i].GetComponent<CanvasGroup>();

            trigger = items[i].gameObject.GetComponent<EventTrigger>();

            entryEnter.Add(new EventTrigger.Entry());
            entryEnter[i].eventID = EventTriggerType.PointerEnter;
            entryEnter[i].callback.AddListener((data) => { DisplayProofsName((PointerEventData)data); });
            trigger.triggers.Add(entryEnter[i]);

            entryExit.Add(new EventTrigger.Entry());
            entryExit[i].eventID = EventTriggerType.PointerExit;
            entryExit[i].callback.AddListener((data) => { HideProofsName((PointerEventData)data); });
            trigger.triggers.Add(entryExit[i]);

            items[i].gameObject.SetActive(true);

        }
    }

    private void OnDisable()
    {
        List<Item> items = null;
        items = ItemsManager.Instance.GetExaminedObjects();
        

        for(int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = Vector3.zero;
            items[i].gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            trigger = items[i].GetComponent<EventTrigger>();
            if (Application.isPlaying)
            {
                Destroy(items[i].gameObject.GetComponent<Draggable>());
            }
            else
            {
                DestroyImmediate(items[i].gameObject.GetComponent<Draggable>());
            }
            

            trigger.triggers.Remove(entryEnter[i]);
            trigger.triggers.Remove(entryExit[i]);


            items[i].gameObject.SetActive(false);
            
        }
    }



    public void DisplayProofsName(PointerEventData ctx)
    {
        proofsInformations.SetActive(true);
        proofsName.text = ctx.pointerEnter.GetComponent<Item>().itemData.itemName;
        proofsInformations.transform.position = ctx.pointerEnter.transform.position - new Vector3(-150, 0);
    }

    public void HideProofsName(PointerEventData ctx)
    {
        proofsName.text = null;
        proofsInformations.SetActive(false);
    }

}
