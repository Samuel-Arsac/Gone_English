using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMap_Button : MonoBehaviour
{
    private GameObject outlineZone;
    [SerializeField] private UIWorldMap.zones linkedZone;

    private void Awake()
    {
        outlineZone = transform.GetChild(0).gameObject;
    }

    public void OutlineZoneDisplay()
    {
        outlineZone.SetActive(true);
    }

    public void OutlineZoneHide()
    {
        outlineZone.SetActive(false);
    }

    public void OnClick(string sceneToLoad)
    {
        
        if (UIWorldMap.Instance.cantGoToRailway && linkedZone == UIWorldMap.zones.Gare)
        {
            DialogueHandler.Instance.StartDialogueCantTravelRailway();
            UIWorldMap.Instance.DisplayUIWordlMap();
            UIManager.Instance.DisableInteractionEnvironnment();
        }

        else if(UIWorldMap.Instance.cantGoToHostel && (linkedZone == UIWorldMap.zones.Hôtel || linkedZone == UIWorldMap.zones.Gare))
        {
            DialogueHandler.Instance.StartDialogueCantTravelAway();
            UIWorldMap.Instance.DisplayUIWordlMap();
            UIManager.Instance.DisableInteractionEnvironnment();
        }

        else if(UIWorldMap.Instance.canTravel)
        {
            UIWorldMap.Instance.currentZone = linkedZone;
            LevelChanger.Instance.FadeToLevel(sceneToLoad);
            UIManager.Instance.DisableInteractionEnvironnment();

        }
        else
        {
            if(UIInventory.Instance.gotWatch)
            {
                
                DialogueHandler.Instance.StartDialogueCantTravelWatch();
            }
            else
            {
                DialogueHandler.Instance.StartDialogueCantTravel();
            }

            UIWorldMap.Instance.DisplayUIWordlMap();
            UIManager.Instance.DisableInteractionEnvironnment();
        }

        //UIWorldMap.Instance.DisplayUIWordlMap();
        OutlineZoneHide();




    }
}
