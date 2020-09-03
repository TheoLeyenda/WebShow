using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text textScore;
    public Text timeParty;
    public Clock clock;
    private void OnEnable()
    {
        PlayerTopDown.OnTakePoint += UpdateScore;
    }
    private void OnDisable()
    {
        PlayerTopDown.OnTakePoint -= UpdateScore;
    }
    public void UpdateScore(InventoryPlayer p)
    {
        if (p != null)
        {
            textScore.text = "" + p.currentCoin;
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
