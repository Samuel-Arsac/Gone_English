// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""eda3d184-22d5-4a67-ab32-2be9b1331aaf"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""b0f3dd9c-1a85-4eba-916f-e949954d87ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""badcfa9c-ae0b-4f6b-8a11-85fa0263b1de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnableDebugMenu"",
                    ""type"": ""Button"",
                    ""id"": ""d09438c7-bc54-407f-9272-68747e20c1c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd0b46b5-9c2b-494c-9179-f4136f6f560c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da8191bc-7fa4-48cb-806a-797259f3f719"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a50dc269-3f54-4dd1-8d3a-9542c51b806d"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableDebugMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Back = m_Keyboard.FindAction("Back", throwIfNotFound: true);
        m_Keyboard_Tab = m_Keyboard.FindAction("Tab", throwIfNotFound: true);
        m_Keyboard_EnableDebugMenu = m_Keyboard.FindAction("EnableDebugMenu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Back;
    private readonly InputAction m_Keyboard_Tab;
    private readonly InputAction m_Keyboard_EnableDebugMenu;
    public struct KeyboardActions
    {
        private @Controls m_Wrapper;
        public KeyboardActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Back => m_Wrapper.m_Keyboard_Back;
        public InputAction @Tab => m_Wrapper.m_Keyboard_Tab;
        public InputAction @EnableDebugMenu => m_Wrapper.m_Keyboard_EnableDebugMenu;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Back.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBack;
                @Tab.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTab;
                @EnableDebugMenu.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEnableDebugMenu;
                @EnableDebugMenu.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEnableDebugMenu;
                @EnableDebugMenu.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEnableDebugMenu;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @EnableDebugMenu.started += instance.OnEnableDebugMenu;
                @EnableDebugMenu.performed += instance.OnEnableDebugMenu;
                @EnableDebugMenu.canceled += instance.OnEnableDebugMenu;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnBack(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnEnableDebugMenu(InputAction.CallbackContext context);
    }
}
