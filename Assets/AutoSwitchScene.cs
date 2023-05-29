using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSwitchScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        LevelChanger.Instance.FadeToLevel(sceneToLoad);
        UIManager.Instance.DisableInteractionEnvironnment();
    }
}
