using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Highlightv2 : MonoBehaviour
{

    public void OnHover(BaseEventData ctx)
    {
        PointerEventData data = ctx as PointerEventData;
        GetComponent<Image>().material.SetInt("_Outline",1);
    }

    public void OnExit(BaseEventData ctx)
    {
        PointerEventData data = ctx as PointerEventData;
        GetComponent<Image>().material.SetInt("_Outline",0);
    }

    public void OnClick(BaseEventData ctx)
    {
        PointerEventData data = ctx as PointerEventData;
        GetComponent<Image>().material.SetInt("_Outline", 0);
    }

}
