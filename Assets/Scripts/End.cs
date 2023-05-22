using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject transitionSection;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject watches;

    public void DisplayTransition()
    {
        transitionSection.SetActive(true);
        watches.SetActive(false);
    }

    public void HideTransition()
    {
        transitionSection.SetActive(false);
        fade.SetActive(true);
        block.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        Destroy(UIManager.Instance.gameObject);
    }
}
