using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorsManager : MonoBehaviour
{
    public static CursorsManager instance;
    [SerializeField] private Texture2D[] cursorsTextures;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero; 
    public bool isDragging;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy (gameObject);
    }

    public void ChangeCursorTexture(int cursorValue)
    {
        Cursor.SetCursor(cursorsTextures[cursorValue], hotSpot, cursorMode);
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void DisplayCursor()
    {
        Cursor.visible = true;
    }

    public void DragCursorDisplay()
    {
        ChangeCursorTexture(4);
    }

    public void DisplayDefaultCursor()
    {
        ChangeCursorTexture(0);
    }

    public void DropCursorDisplay()
    {
        ChangeCursorTexture(5);
    }
}
