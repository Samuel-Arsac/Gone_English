using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFeedback : MonoBehaviour
{
    public void CallHideFeedback()
    {
        UIManager.Instance.HideFeedback();
    }
}
