using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Sections")]
    [SerializeField] private GameObject mainSection;
    [SerializeField] private GameObject optionsSection;
    [SerializeField] private GameObject creditsSection;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private string sceneToLoad = "Introduction";

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void DisplayCredits()
    {
        mainSection.SetActive(false);
        creditsSection.SetActive(true);
    }

    public void HideCredits()
    {
        mainSection.SetActive(true);
        creditsSection.SetActive(false);
    }

    public void DisplayOptionsSection()
    {
        mainSection.SetActive(false);
        optionsSection.SetActive(true);
    }

    public void HideOptionsSection()
    {
        mainSection.SetActive(true);
        optionsSection.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
