using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIProofs : MonoBehaviour
{
    [SerializeField] private List<Transform> slotsLists;
    [SerializeField] private List<Transform> proofsInformationsLists;
    [SerializeField] private GameObject proofsInformations;
    [SerializeField] private TextMeshProUGUI proofsName;

    [SerializeField] private GridLayoutGroup grid;

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
        if (UIManager.Instance.GetIntialisation())
        {
            AudioManager.Instance.PlaySFX("Book");
        }

        List<Item> proofs = null;

        entryEnter = new List<EventTrigger.Entry>();
        entryExit = new List<EventTrigger.Entry>();


        proofs = ItemsManager.Instance.GetExaminedObjects();

        for(int i = 0; i < proofs.Count; i++)
        {
            proofs[i].transform.position = slotsLists[i].position;
            proofs[i].transform.localScale = slotsLists[i].localScale;

            trigger = proofs[i].gameObject.GetComponent<EventTrigger>();

            entryEnter.Add(new EventTrigger.Entry());
            entryEnter[i].eventID = EventTriggerType.PointerEnter;
            entryEnter[i].callback.AddListener((data) => {DisplayProofsName((PointerEventData)data);});
            trigger.triggers.Add(entryEnter[i]);

            entryExit.Add(new EventTrigger.Entry());
            entryExit[i].eventID = EventTriggerType.PointerExit;
            entryExit[i].callback.AddListener((data) => {HideProofsName((PointerEventData)data);});
            trigger.triggers.Add(entryExit[i]);

            proofs[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable() 
    {
        List<Item> proofs = null;

        proofs = ItemsManager.Instance.GetExaminedObjects();
        for(int i = 0; i < proofs.Count; i++)
        {
            proofs[i].transform.position = Vector3.zero;
            proofs[i].gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            trigger = proofs[i].gameObject.GetComponent<EventTrigger>();

            trigger.triggers.Remove(entryEnter[i]);
            trigger.triggers.Remove(entryExit[i]);


            proofs[i].gameObject.SetActive(false);
        }
    }

    public void DisplayProofsName(PointerEventData ctx)
    {
        proofsInformations.SetActive(true);
        proofsName.text = ctx.pointerEnter.GetComponent<Item>().itemData.itemName;
        proofsInformations.transform.position = ctx.pointerEnter.transform.position - new Vector3(0, 100);
    }

    public void HideProofsName(PointerEventData ctx)
    {
        proofsName.text = null;
        proofsInformations.SetActive(false);
    }


}
