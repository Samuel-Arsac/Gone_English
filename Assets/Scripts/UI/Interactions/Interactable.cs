using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private int interactableType;

    public void EnterObject()
    {
        switch(interactableType)
        {
            case 1:
                CursorsManager.instance.ChangeCursorTexture(1);
                break;
            case 2:
                CursorsManager.instance.ChangeCursorTexture(2);
                break;
            case 3:
                CursorsManager.instance.ChangeCursorTexture(1);
                break;
            case 4:
                CursorsManager.instance.ChangeCursorTexture(1);
                break;
        }
    }

    public void OnClick()
    {
        switch(interactableType)
        {
            case 1:
                ItemsManager.Instance.InstantiateObject(name);
                UIManager.Instance.DisplayGetItemText(name);
                Destroy (gameObject);
                break;

            case 2:
                UIManager.Instance.DisableInteractionEnvironnment();
                DialogueHandler.Instance.characterInfo = GetComponent<CharacterInfo>();
                UIManager.Instance.clickedCharacter = gameObject;
                CameraManager.instance.virtualCameraZoom.m_Follow = gameObject.transform;
                CameraManager.instance.virtualCameraZoom.m_Priority += 10;
                StartCoroutine(DelayBeforeDialogue());
                break;

            case 3:
                UIManager.Instance.DisableInteractionEnvironnment();
                DialogueHandler.Instance.characterInfo = GetComponent<CharacterInfo>();
                UIManager.Instance.clickedCharacter = gameObject;
                CameraManager.instance.virtualCameraZoom.m_Follow = gameObject.transform;
                CameraManager.instance.virtualCameraZoom.m_Priority += 10;
                StartCoroutine(DelayBeforeDialogue());
                break;

            case 4:
                UIManager.Instance.DisableInteractionEnvironnment();
                DialogueHandler.Instance.characterInfo = GetComponent<CharacterInfo>();
                UIManager.Instance.clickedCharacter = gameObject;
                CameraManager.instance.virtualCameraZoom.m_Follow = gameObject.transform;
                CameraManager.instance.virtualCameraZoom.m_Priority += 10;
                StartCoroutine(DelayBeforeDialogue());
                break;

        }
        ExitObject();
        
    }

    private void StartDialogue()
    {
        DialogueHandler.Instance.startDialogue(DialogueHandler.Instance.characterInfo, DialogueHandler.Instance.characterInfo.isAlone);
    }

    IEnumerator DelayBeforeDialogue()
    {
        yield return new WaitForSeconds(CameraManager.instance.cinemachineBrain.m_DefaultBlend.m_Time);
        if(interactableType == 4)
        {
            EndLoader.Instance.EnableAnimator();
        }
        else
        {
            StartDialogue();
        }
        
    }

    public void ExitObject()
    {   
        CursorsManager.instance.ChangeCursorTexture(0);
    }
}
