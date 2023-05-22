using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Droppable : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData cxt)
    {
        if(CursorsManager.instance.isDragging)
        {
            GetComponent<Image>().material.SetInt("_Outline", 1);
        }
        else
        return;
    }

    public void OnPointerExit(PointerEventData cxt)
    {
        if(CursorsManager.instance.isDragging)
        {
            GetComponent<Image>().material.SetInt("_Outline", 0);
        }
        else
        return;
    }

    public void OnDrop(PointerEventData ctx)
    {
        CursorsManager.instance.isDragging = false;
        CursorsManager.instance.DropCursorDisplay();
        GetComponent<Image>().material.SetInt("_Outline",0);
        if(DialogueHandler.Instance.characterInfo.presentLists[0].objectToProgress == 
        UIScrollingMenu.instance.draggedObject.GetComponent<Item>().itemData.itemName)
        {
            
            DialogueHandler.Instance.GoodProofPresentation();
        }
        else
        {
            DialogueHandler.Instance.ContinueDialogue();
        }
    }
}
