using UnityEngine;
using UnityEngine.EventSystems;

public class InputClick : MonoBehaviour, IPointerClickHandler
{
    private CharacterInfo dialogueList;
    public float intrestPointType;
    [HideInInspector] public IntrestPoint informationsPoints;

    private void Start() 
    {
        dialogueList = transform.GetChild(0).GetComponent<CharacterInfo>();
        UIInventory.Instance.itemExamined.itemData.isExamined = true;
    }

    public void OnPointerClick(PointerEventData ctx)
    {
        switch(intrestPointType)
        {
            case 1:
                if(informationsPoints.hasBeenChecked)
                {
                    ExamenManager.Instance.ClickMinorPoint(informationsPoints);
                }
                else
                {
                    informationsPoints.hasBeenChecked = true;
                    ExamenManager.Instance.ClickMajorPoint(dialogueList);
                }
                
                break;
            case 2:
                ExamenManager.Instance.ClickMinorPoint(informationsPoints);
                break;
        }
    }

}