using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Summary", menuName = "SO/Profiles")]
public class Profiles : ScriptableObject
{
    public string characterName;
    [TextArea]
    public string characterDescriptions;
    public Sprite characterSprite;
}
