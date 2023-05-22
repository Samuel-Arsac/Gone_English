using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Summary", menuName = "SO/Summary")]
public class DailySummary : ScriptableObject
{

    public string numberDay;
    [TextArea]public string summary;

}
