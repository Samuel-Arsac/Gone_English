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
    }

    public void LoadRailway()
    {
        SceneManager.LoadScene("Gare");
    }

    public void LoadHotel()
    {
        SceneManager.LoadScene("Hôtel");
    }
    public void LoadPlace()
    {
        SceneManager.LoadScene("GrandePlace");
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }

}
