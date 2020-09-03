using UnityEngine;
using UnityEngine.UI;
using Players;
public class UI_Manager : MonoBehaviour
{
    public Text textScore;
    public Text textNeedScore;
    public Text timeParty;
    public Clock clock;
    private int countNeedScore = 0;
    private void OnEnable()
    {
        PlayerTopDown.OnTakePoint += UpdateScore;
        TNT_Floor.GameManager.settingNeededPointsForFinishLevel += SetCountNeedScore;
    }
    private void OnDisable()
    {
        PlayerTopDown.OnTakePoint -= UpdateScore;
        TNT_Floor.GameManager.settingNeededPointsForFinishLevel -= SetCountNeedScore;
    }
    public void SetCountNeedScore(TNT_Floor.GameManager manager)
    {
        countNeedScore = manager.neededPointsForFinishLevel;
    }
    public void UpdateScore(InventoryPlayer p)
    {
        if (p != null)
        {
            if (textScore != null)
            {
                textScore.text = "" + p.currentCoin;
            }

            if (textNeedScore != null)
            {
                if(p.currentCoin <= countNeedScore)
                textNeedScore.text = p.currentCoin + "/" + countNeedScore;
            }
        }
    }
    private void Update()
    {
        CheckTime();
    }
    public void CheckTime()
    {
        if(clock.minutes > 0 || clock.seconds > 0)
            ClockFunction();
    }
    public void ClockFunction()
    {
        if (Mathf.Round(clock.seconds) >= 10)
        {
            timeParty.text = Mathf.Round(clock.minutes) + ":" + Mathf.Round(clock.seconds);
        }
        else
        {
            timeParty.text = Mathf.Round(clock.minutes) + ":0" + Mathf.Round(clock.seconds);
        }
    }
}
