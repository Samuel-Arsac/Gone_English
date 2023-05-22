using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public Items_SO itemData;
    private void Awake()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry tmpEntry = trigger.triggers.Find(t => t.eventID == EventTriggerType.PointerClick);
        if(tmpEntry == null)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            trigger.triggers.Add(entry);
        }
    }
    
}
