using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Items", menuName = "SO/Items")]
public class Items_SO : ScriptableObject
{
    public string itemName;
    public string itemSubtitle;
    public bool readDescription = false;
    [TextArea]public string itemDescription;
    public bool canExamine;
    [ShowIf("canExamine")][AllowNesting]
    public GameObject pastItem;
    [ShowIf("canExamine")][AllowNesting]
    [TextArea] public string descriptionPostExamen;

    public bool isExamined;
    public bool isProofs;
    
}
