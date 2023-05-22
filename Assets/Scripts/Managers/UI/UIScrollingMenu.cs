using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollingMenu : MonoBehaviour
{
    [HideInInspector]public GameObject draggedObject = null;
    [SerializeField] private GameObject UIMenuScrolling;
    
    #region Singleton
    public static UIScrollingMenu instance;
    void Awake ()
    {
        if (instance == null)
        instance = this;
        else
        Destroy (gameObject);
    }

    #endregion

   
    public void DisplayPresentMenu()
    {
        UIMenuScrolling.SetActive(true);
        UIManager.Instance.DisableInventoryButtonInteraction();
        UIManager.Instance.DisableDiaryButtonInteraction();
        UIManager.Instance.DisableMapButtonInteraction();
    }

    public void HidePresentMenu()
    {
        UIMenuScrolling.SetActive(false);
        UIManager.Instance.EnableInventoryButtonInteraction();
        UIManager.Instance.EnableDiaryButtonInteraction();
        UIManager.Instance.EnableMapButtonInteraction();
    }
}
