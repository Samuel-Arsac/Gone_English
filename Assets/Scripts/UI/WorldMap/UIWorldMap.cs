using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorldMap : LocalManager<UIWorldMap>
{
    public enum zones
    {
        Hôtel = 0, 
        Gare = 1, 
        GrandePlace = 2, 
        Hangars = 3, 
        Usine = 4, 
        Marché = 5
    };

    [SerializeField] List<GameObject> buttonsZones;
    public zones currentZone;

    private bool isWorldMapOpen = false;
    public bool canTravel = true;



    [HideInInspector]public bool placeUnlocked = false;
    [HideInInspector]public bool hangarsUnlocked = false;
    [HideInInspector]public bool factoriesUnlocked = false;
    [HideInInspector]public bool marketUnlocked = false;

    [HideInInspector] public bool cantGoToRailway = false;
    [HideInInspector] public bool cantGoToHostel = false;

    private void OnEnable() 
    {
        switch(currentZone)
        {
            case zones.Gare:
                DisableActiveZoneButton(buttonsZones[0]);
                EnableActiveZoneButton(buttonsZones[1]);
                EnableActiveZoneButton(buttonsZones[2]);
                EnableActiveZoneButton(buttonsZones[3]);
                EnableActiveZoneButton(buttonsZones[4]);
                EnableActiveZoneButton(buttonsZones[5]);
                break;

            case zones.Hôtel:
                DisableActiveZoneButton(buttonsZones[1]);
                EnableActiveZoneButton(buttonsZones[0]);
                EnableActiveZoneButton(buttonsZones[2]);
                EnableActiveZoneButton(buttonsZones[3]);
                EnableActiveZoneButton(buttonsZones[4]);
                EnableActiveZoneButton(buttonsZones[5]);
                cantGoToRailway = true;
                break;

            case zones.GrandePlace:
                DisableActiveZoneButton(buttonsZones[2]);
                EnableActiveZoneButton(buttonsZones[0]);
                EnableActiveZoneButton(buttonsZones[1]);
                EnableActiveZoneButton(buttonsZones[3]);
                EnableActiveZoneButton(buttonsZones[4]);
                EnableActiveZoneButton(buttonsZones[5]);
                cantGoToHostel = true;
                cantGoToRailway = false;
                break;

            case zones.Hangars:
                DisableActiveZoneButton(buttonsZones[3]);
                EnableActiveZoneButton(buttonsZones[0]);
                EnableActiveZoneButton(buttonsZones[1]);
                EnableActiveZoneButton(buttonsZones[2]);
                EnableActiveZoneButton(buttonsZones[4]);
                EnableActiveZoneButton(buttonsZones[5]);
                canTravel = false;
                cantGoToHostel = false;
                break;

            case zones.Usine:
                DisableActiveZoneButton(buttonsZones[4]);
                EnableActiveZoneButton(buttonsZones[0]);
                EnableActiveZoneButton(buttonsZones[1]);
                EnableActiveZoneButton(buttonsZones[2]);
                EnableActiveZoneButton(buttonsZones[3]);
                EnableActiveZoneButton(buttonsZones[5]);
                break;
            case zones.Marché:
                DisableActiveZoneButton(buttonsZones[5]);
                EnableActiveZoneButton(buttonsZones[0]);
                EnableActiveZoneButton(buttonsZones[1]);
                EnableActiveZoneButton(buttonsZones[2]);
                EnableActiveZoneButton(buttonsZones[3]);
                EnableActiveZoneButton(buttonsZones[4]);
                break;
        }

        if(placeUnlocked)
        {
            if(currentZone == zones.GrandePlace)
            {
                buttonsZones[2].GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                buttonsZones[2].GetComponent<Image>().raycastTarget = true;
            }
            
            buttonsZones[2].transform.GetChild(2).gameObject.SetActive(false);
            buttonsZones[2].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            buttonsZones[2].GetComponent<Image>().raycastTarget = false;
            buttonsZones[2].transform.GetChild(2).gameObject.SetActive(true);
            buttonsZones[2].transform.GetChild(1).gameObject.SetActive(false);
        }

        if(hangarsUnlocked)
        {
            if (currentZone == zones.Hangars)
            {
                buttonsZones[3].GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                buttonsZones[3].GetComponent<Image>().raycastTarget = true;
            }

            buttonsZones[3].transform.GetChild(2).gameObject.SetActive(false);
            buttonsZones[3].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            buttonsZones[3].GetComponent<Image>().raycastTarget = false;
            buttonsZones[3].transform.GetChild(2).gameObject.SetActive(true);
            buttonsZones[3].transform.GetChild(1).gameObject.SetActive(false);
        }

        if(factoriesUnlocked)
        {
            if (currentZone == zones.Usine)
            {
                buttonsZones[4].GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                buttonsZones[4].GetComponent<Image>().raycastTarget = true;
            }

            buttonsZones[4].transform.GetChild(2).gameObject.SetActive(false);
            buttonsZones[4].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            buttonsZones[4].GetComponent<Image>().raycastTarget = false;
            buttonsZones[4].transform.GetChild(2).gameObject.SetActive(true);
            buttonsZones[4].transform.GetChild(1).gameObject.SetActive(false);
        }

        if (marketUnlocked)
        {
            if (currentZone == zones.Marché)
            {
                buttonsZones[5].GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                buttonsZones[5].GetComponent<Image>().raycastTarget = true;
            }

            buttonsZones[5].transform.GetChild(2).gameObject.SetActive(false);
            buttonsZones[5].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            buttonsZones[5].GetComponent<Image>().raycastTarget = false;
            buttonsZones[5].transform.GetChild(2).gameObject.SetActive(true);
            buttonsZones[5].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void DisplayUIWordlMap()
    {
        if(UIManager.Instance.GetIntialisation())
        {
            AudioManager.Instance.PlaySFX("Map");
        }
        
        if(UIManager.Instance.currentMenuOpen != null && UIManager.Instance.currentMenuOpen.name != gameObject.name)
        {
            UIManager.Instance.CheckIfMenuIsOpen(gameObject);
        }
        
        if (isWorldMapOpen)
        {
            UIManager.Instance.EnableInteractionEnvironnment();
            if(UIManager.Instance.isInspectingEnviro)
            {
                UIManager.Instance.DisplayEnviroCursor();
            }
            

            gameObject.SetActive(false);
            
            isWorldMapOpen = false;

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
            

            gameObject.SetActive(true);
            
            isWorldMapOpen = true;

            UIManager.Instance.menuAlreadyOpen = true;
            UIManager.Instance.currentMenuOpen = this.gameObject;
        }
    }

    
    public void EnableActiveZoneButton(GameObject button)
    {
        button.transform.GetChild(0).gameObject.SetActive(false);
        button.GetComponent<Image>().raycastTarget = true;

        Image textButton = button.transform.GetChild(1).GetComponent<Image>();
        textButton.raycastTarget = true;
        textButton.color = Color.white;
    }

    public void DisableActiveZoneButton(GameObject button)
    {
        button.transform.GetChild(0).gameObject.SetActive(true);
        button.GetComponent<Image>().raycastTarget = false;

        Image textButton = button.transform.GetChild(1).GetComponent<Image>();
        textButton.raycastTarget = false;
        textButton.color = Color.gray;
    }

    public void SwitchZone(string zoneName)
    {
        currentZone = (zones)System.Enum.Parse(typeof(zones), zoneName);
    }

}
