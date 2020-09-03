using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TNTFloorManager floorManager;
    public CoinsGenerator coins;
    public GameObject doorExit;
    public GameObject arrowExit;
    public bool DisableCoins = true;
    private void OnEnable()
    {
        Clock.OnFinishClock += FinishGame;
        if (arrowExit != null)
        {
            arrowExit.SetActive(false);
        }
        if (doorExit != null)
        {
            doorExit.SetActive(false);
        }
    }
    private void OnDisable()
    {
        Clock.OnFinishClock -= FinishGame;
    }
    public void FinishGame(Clock clock)
    {
        if (doorExit != null)
        {
            doorExit.SetActive(true);
        }
        if (arrowExit != null)
        {
            arrowExit.SetActive(true);
        }
        for (int i = 0; i < floorManager.tnt_FloorList.Count; i++)
        {
            if (floorManager.tnt_FloorList[i] != null)
            {
                floorManager.tnt_FloorList[i].stateTNT = TNT_Floor.StateTNT.Normal;
                floorManager.tnt_FloorList[i].enableUpdate = false;
            }
        }
        for (int i = 0; i < floorManager.tnt_ExplotedList.Count; i++)
        {
            if (floorManager.tnt_ExplotedList[i] != null)
            {
                floorManager.tnt_ExplotedList[i].stateTNT = TNT_Floor.StateTNT.Normal;
                floorManager.tnt_ExplotedList[i].enableUpdate = false;
            }
        }
        if (coins != null && DisableCoins)
        {
            coins.poolCoin.DisableObjectsPool();
        }
    }
}
