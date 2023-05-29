using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenu : ProjectManager<DebugMenu>
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void DisplayDebugMenu()
    {
        InputsManager.Instance.controls.Keyboard.EnableDebugMenu.performed -= InputsManager.Instance.DebugMenuDisplay;
        InputsManager.Instance.controls.Keyboard.EnableDebugMenu.performed += InputsManager.Instance.DebugMenuHide;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideDebugMenu()
    {
        InputsManager.Instance.controls.Keyboard.EnableDebugMenu.performed += InputsManager.Instance.DebugMenuDisplay;
        InputsManager.Instance.controls.Keyboard.EnableDebugMenu.performed -= InputsManager.Instance.DebugMenuHide;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void LoadHangars()
    {
        SceneManager.LoadScene("Hangars");
        HideDebugMenu();
    }

    public void LoadRailway()
    {
        SceneManager.LoadScene("Gare");
        HideDebugMenu();
    }

    public void LoadHotel()
    {
        SceneManager.LoadScene("Hôtel");
        HideDebugMenu();
    }
    public void LoadPlace()
    {
        SceneManager.LoadScene("GrandePlace");
        HideDebugMenu();
    }

    public void LoadChase()
    {
        SceneManager.LoadScene("GrandePlace_Chase");
        HideDebugMenu();
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
        HideDebugMenu();
    }

}
