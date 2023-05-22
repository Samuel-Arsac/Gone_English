using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLoader : ProjectManager<EndLoader>
{
    [SerializeField] private Animator fadeAnimator;

    public void EnableAnimator()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        fadeAnimator.enabled = true;
    }

    public void StartEndDialogue()
    {
        DialogueHandler.Instance.startDialogue(DialogueHandler.Instance.characterInfo, true);
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void LoadEnd()
    {
        UIManager.Instance.DisableButtons();
        SceneManager.LoadScene("End");
    }
}
