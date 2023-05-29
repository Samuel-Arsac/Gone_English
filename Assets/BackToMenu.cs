using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private void Start()
    {
        Destroy(UIManager.Instance.gameObject);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.SwapMusic("MainTheme2");
    }
}
