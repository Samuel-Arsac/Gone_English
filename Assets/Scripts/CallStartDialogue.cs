using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallStartDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject oui = GameObject.FindGameObjectWithTag("Destroy");
        oui.GetComponent<DestroyOld>().enabled = true;

        
    }
}
