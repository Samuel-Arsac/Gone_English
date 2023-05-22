using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelLock : MonoBehaviour
{
    private void Start()
    {
        UIWorldMap.Instance.canTravel = false;
    }
}
