using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class ScenesManager : ProjectManager<ScenesManager>
{
    [SerializeField] private CharacterInfo cantTravelDialogue;


    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Leaving game");
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    /*public void LoadNewZone(string sceneToLoad)
    {
        UIWorldMap.Instance.DisplayUIWordlMap();
        if (UIWorldMap.Instance.canTravel)
        {
            LevelChanger.Instance.FadeToLevel(sceneToLoad);

        }
        else
        {
            DialogueHandler.Instance.StartDialogueCantTravelWatch();
        }
        
    }*/

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIManager.Instance.worldCanvasGameObject = GameObject.FindGameObjectWithTag("World Canvas");
        UIManager.Instance.presentInspeciton = GameObject.FindGameObjectWithTag("Present");
        UIManager.Instance.pastInspection = GameObject.FindGameObjectWithTag("Past");
        UIManager.Instance.SetInspectionZone();

        UIManager.Instance.SetCanvasInteractable();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Gare");
    }
}
