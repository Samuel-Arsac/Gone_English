using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : LocalManager<LevelChanger>
{
    [SerializeField] private Animator fadeAnimator;
    private string sceneToLoad;
    private CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void FadeOutBegins()
    {
        EnableCanvasGroup();
        AudioManager.Instance.PlaySFX("PetraFoot");
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnFadeInBegins()
    {
        AudioManager.Instance.PlaySFX("PetraFoot");
        UIManager.Instance.HideIcons();
        UIManager.Instance.DisableInteractionEnvironnment();
    }

    public void OnFadeInComplete()
    {
        UIManager.Instance.DisplayIcons();
        DisableCanvasGroup();
    }

    public void DisableCanvasGroup()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void EnableCanvasGroup()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void FadeToLevel(string sceneName)
    {
        UIManager.Instance.DisableInteractionEnvironnment();
        UIManager.Instance.HideIcons();
        fadeAnimator.SetTrigger("FadeOut");
        sceneToLoad = sceneName;
    }
}
