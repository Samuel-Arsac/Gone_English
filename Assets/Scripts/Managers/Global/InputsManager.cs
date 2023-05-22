
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class InputsManager :  LocalManager<InputsManager>
{
    [SerializeField] private InputSystemUIInputModule UIModule;
    public Controls controls;
    [SerializeField] private bool enableLog;
    [SerializeField] private bool enableDebug;


    protected override void Awake()
    {
        base.Awake();
        controls = new Controls();

        controls.Keyboard.Back.performed += EscapeClick;
        if(enableLog)
        {
            controls.Keyboard.Tab.performed += LogClick;
        }
        if(enableDebug)
        {
            controls.Keyboard.EnableDebugMenu.performed += DebugMenuDisplay;
        }
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    public void ChangeLeftClickAction(CharacterInfo dialogueList)
    {
        UIModule.leftClick.action.Enable();
        UIModule.leftClick.action.performed += _ => ExamenManager.Instance.ClickMajorPoint(dialogueList);
    }

    public void RemoveLeftClickAction(CharacterInfo dialogueList)
    {
        UIModule.leftClick.action.started -= _ => ExamenManager.Instance.ClickMajorPoint(dialogueList);
        UIModule.leftClick.action.Disable();
    }

    public Vector2 ReadMousePostionValue()
    {
        return UIModule.point.action.ReadValue<Vector2>();
    }

    public Vector2 ReadMousePositionValueWorldSpace()

    {
        Vector2 mousePos = UIModule.point.action.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPos;
    }

    public void EscapeClick(InputAction.CallbackContext context)
    {
        UIManager.Instance.DisplayQuitConfirm();
    }

    public void EscapeLeave(InputAction.CallbackContext context)
    {
        UIManager.Instance.HideQuitConfirm();

    }

    public void LogClick(InputAction.CallbackContext context)
    {
        UIManager.Instance.DisplayLogSection();
    }

    public void LogLeave(InputAction.CallbackContext context)
    {
        UIManager.Instance.HideLogSection();
    }

    public void DebugMenuDisplay(InputAction.CallbackContext context)
    {
        DebugMenu.Instance.DisplayDebugMenu();
    }

    public void DebugMenuHide(InputAction.CallbackContext context)
    {
        DebugMenu.Instance.HideDebugMenu();
    }
}
