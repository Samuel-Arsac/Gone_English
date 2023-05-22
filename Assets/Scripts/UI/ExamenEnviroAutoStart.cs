using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamenEnviroAutoStart : MonoBehaviour
{

    private void Start()
    {
        UIManager.Instance.CallFade();
    }

}
