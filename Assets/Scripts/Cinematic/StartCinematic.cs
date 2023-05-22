using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    private CinematicDialogue dialogues;
    [SerializeField] private string musicToStop;
    [SerializeField] private string musicToPlay; 
    // Start is called before the first frame update
    void Start()
    {
        dialogues = CinematicHandler.Instance.cinematicDialogues;
        StartCoroutine(AudioManager.Instance.Stop(musicToStop));

        /*AudioManager.Instance.SwapMusic(musicToPlay);*/

        AudioManager.Instance.PlaySFX("Alarm");
        AudioManager.Instance.PlaySFX("InTrain_03");

        if(CinematicHandler.Instance.autoLaunch)
        {
            UIIntro.Instance.HideContinueZone();
            StartCoroutine(waitToStart());
        }
    }
    
    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(CinematicHandler.Instance.timeToWait);
        UIIntro.Instance.DisplayContinueZone();
        CinematicHandler.Instance.startDialogue(dialogues);
    }
}
