using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableItemsExamen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIInventory.Instance.UnlockWatch();
    }
}
