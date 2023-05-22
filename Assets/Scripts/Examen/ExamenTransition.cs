using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamenTransition : MonoBehaviour
{
    [SerializeField] private bool enviro;
    private void OnEnable()
    {
        UIManager.Instance.DisableInteractionEnvironnment();
        UIManager.Instance.HideIcons();
    }

    private void OnDisable()
    {
        UIManager.Instance.EnableInteractionEnvironnment();        
    }

    public void StartExamen()
    {
        if(enviro)
        {
            UIManager.Instance.EnableEnvironementExamen();
            UIManager.Instance.HideTransition();
            
        }
        else
        {
            UIInventory.Instance.HideTransition();
        }
    }
}
