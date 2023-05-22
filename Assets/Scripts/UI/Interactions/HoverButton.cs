using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    [SerializeField] private Sprite highlightSprite;
    [SerializeField] private GameObject linkedSection;
    private Sprite normalSprite;
    private Button button;
    private Image currentImage;

    private void Start() 
    {
        currentImage = GetComponent<Image>();
        normalSprite = currentImage.sprite;
        button = GetComponent<Button>();
    }

    public void OnHover()
    {
        currentImage.sprite = highlightSprite;
    }

    public void OnClick(BaseEventData ctx)
    {
        PointerEventData data = ctx as PointerEventData;

        CanvasGroup clickedButton = data.pointerPress.GetComponent<CanvasGroup>();

        //UIDiary.Instance.DisplayLastSectionOpened(linkedSection, clickedButton);
    }

    public void ButtonClick()
    {
        button.interactable = false;
        UIDiary.Instance.DisplayLastSectionOpened(linkedSection, button);
    }

    public void OnExit()
    {
        currentImage.sprite = normalSprite;
    }
}
