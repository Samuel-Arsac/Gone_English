using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour
{

    public void OnHover(BaseEventData ctx)
    {
        if (UIManager.Instance.isInspectingEnviro)
        {
            CursorsManager.instance.DisplayCursor();
            UIManager.Instance.HideEnviroCursor();
        }

        PointerEventData data = ctx as PointerEventData;
        GetComponent<Image>().material.SetInt("_Outline",1);
    }

    public void OnExit(BaseEventData ctx)
    {
        if(UIManager.Instance.isInspectingEnviro)
        {
            CursorsManager.instance.HideCursor();
            UIManager.Instance.DisplayEnviroCursor();
        }
        PointerEventData data = ctx as PointerEventData;
        GetComponent<Image>().material.SetInt("_Outline",0);
    }

    public void OnClick(BaseEventData ctx)
    {
        PointerEventData data = ctx as PointerEventData;
        GetComponent<Image>().material.SetInt("_Outline", 0);
    }

}
