using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEntryDiary : MonoBehaviour
{
    [SerializeField] private List<DailySummary> newEntries;
    [SerializeField] private List<Profiles> newCharacters;

    private void OnEnable()
    {
        for(int i = 0; i < newEntries.Count; i++)
        {
            if(newEntries != null)
            {
                UIDaily.Instance.dailySummaries.Add(newEntries[i]);
            }          
        }

        for(int i = 0; i < newCharacters.Count; i++)
        {
            if (newCharacters != null)
            {
                UIProfiles.Instance.GetCharactersProfiles().Add(newCharacters[i]);
            }
        }

        AudioManager.Instance.PlaySFX("Writing");
        UIManager.Instance.DisplayFeedback();
    }
}
