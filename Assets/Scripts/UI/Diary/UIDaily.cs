using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIDaily : LocalManager<UIDaily>
{

    public List<DailySummary> dailySummaries;
    [SerializeField] private TextMeshProUGUI dayNumberText;
    [SerializeField] private TextMeshProUGUI summaryText;
    [SerializeField] private TextMeshProUGUI numberPageText;

    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    private int numberDay;

    private void OnEnable() 
    {
        if(UIManager.Instance.GetIntialisation())
        {
            AudioManager.Instance.PlaySFX("Book");
        }

        numberDay = dailySummaries.Count -  1;
        UpdateDaySummaryText();

    }

    public void UpdateDaySummaryText()
    {
        if(numberDay >= 1)
        {
            leftArrow.SetActive(true);
        }
        else
        {
            leftArrow.SetActive(false);
        }

        if(numberDay >= dailySummaries.Count-1)
        {
            rightArrow.SetActive(false);
        }
        else
        {
            rightArrow.SetActive(true);
        }


        dayNumberText.text = dailySummaries[numberDay].numberDay.ToString();
        summaryText.text = dailySummaries[numberDay].summary;
        if(dailySummaries.Count >= 10)
        {
            numberPageText.text = "Page " + (numberDay + 1);
        }
        else
        {
            numberPageText.text = "Page 0" + (numberDay + 1);
        }
        
        //numberPageText.text = "0" + (numberDay+1) + " - 0" + dailySummaries.Count;
    }

    public void NextPage()
    {
        numberDay++;
        UpdateDaySummaryText();
    }

    public void LastPage()
    {
        numberDay--;
        UpdateDaySummaryText();
    }


}
