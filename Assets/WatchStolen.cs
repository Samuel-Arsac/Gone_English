using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchStolen : MonoBehaviour
{
    private void Start()
    {
        UIInventory.Instance.gotWatch = false;
        if(UIManager.Instance.isInspectingEnviro)
        {
            UIManager.Instance.EnableEnvironementExamen();
        }
        UIManager.Instance.HideWatchButton();
    }

}
