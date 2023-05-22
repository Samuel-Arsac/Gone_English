using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    Vector2 _originalPosition;
    public CanvasGroup itemCanvasGroup;

    private void Awake() 
    {
        _originalPosition = transform.position;    
    }

    public void OnPointerEnter(PointerEventData ctx)
    {
        CursorsManager.instance.DropCursorDisplay();
    }

    public void OnPointerExit(PointerEventData cxt)
    {
        StopAllCoroutines();
        CursorsManager.instance.DisplayDefaultCursor();
    }

    public void OnPointerDown(PointerEventData ctx)
    {
        CursorsManager.instance.DragCursorDisplay();
    }

    public void OnPointerUp(PointerEventData ctx)
    {
        CursorsManager.instance.DisplayDefaultCursor();
    }

    public void OnBeginDrag(PointerEventData ctx)
    {
        
        UIScrollingMenu.instance.draggedObject = gameObject;
        itemCanvasGroup.blocksRaycasts = false;
        CursorsManager.instance.isDragging = true;
    }

    public void OnDrag(PointerEventData ctx)
    {
        transform.position = ctx.position;
        CursorsManager.instance.DragCursorDisplay();
    }
    public void OnEndDrag(PointerEventData ctx)
    {
        UIScrollingMenu.instance.draggedObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.position  = _originalPosition;
        UIScrollingMenu.instance.draggedObject = null;
        CursorsManager.instance.isDragging = false; 
        StartCoroutine(delayBeforeResetCursor());
    }

    IEnumerator delayBeforeResetCursor()
    {
        CursorsManager.instance.DropCursorDisplay();
        yield return new WaitForSeconds(0.5f);
        CursorsManager.instance.DisplayDefaultCursor();
    }
}
