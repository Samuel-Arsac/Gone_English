using UnityEngine;
using UnityEngine.EventSystems;

public class InputClickEnvironnement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject pastKatie;
    [SerializeField] private GameObject pastKeoni;
    [SerializeField] private CharacterInfo dialoguePast;
    [SerializeField] private GameObject cursorFind;
    private int interlocutor = 0;
    private bool isHovering;
    private Interactable interactableInfos;

    private void Start()
    {
        interactableInfos = pastKatie.GetComponent<Interactable>();
    }

    public void OnPointerClick(PointerEventData ctx)
    {
        if(isHovering)
        {
            if(interlocutor == 1)
            {
                UIManager.Instance.clickedCharacter = pastKatie;
            }
            else if(interlocutor == 2)
            {
                UIManager.Instance.clickedCharacter = pastKeoni;
            }

            interactableInfos.OnClick();
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        if ((RectTransformUtility.RectangleContainsScreenPoint(UIManager.Instance.GetInspectorCursorPosition(), pastKatie.transform.position)) ||
            (RectTransformUtility.RectangleContainsScreenPoint(UIManager.Instance.GetInspectorCursorPosition(), pastKeoni.transform.position)))
        {
            isHovering = true;
            UIManager.Instance.GetInspectorCursorPosition().gameObject.SetActive(false);
            UIManager.Instance.GetInspectionCursorFind().SetActive(true);
            UIManager.Instance.GetInspectionCursorFind();
            if((RectTransformUtility.RectangleContainsScreenPoint(UIManager.Instance.GetInspectorCursorPosition(), pastKatie.transform.position)))
            {
                interlocutor = 1;
            }

            else if((RectTransformUtility.RectangleContainsScreenPoint(UIManager.Instance.GetInspectorCursorPosition(), pastKeoni.transform.position)))
            {
                interlocutor = 2;
            }

        }
        else
        {
            isHovering = false;
            UIManager.Instance.GetInspectorCursorPosition().gameObject.SetActive(true);
            UIManager.Instance.GetInspectionCursorFind().SetActive(false);
            return;
        }
    }

}