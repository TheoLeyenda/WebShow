using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace TNT_Floor
{
    using Players;
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public TNTFloorManager floorManager;
        public CoinsGenerator coins;
        public GameObject doorExit;
        public GameObject arrowExit;
        public bool DisableCoins = true;
        public int neededPointsForFinishLevel;
        private bool finishTime = false;
        private bool neededPoints = false;
        public static event Action<GameManager> settingNeededPointsForFinishLevel;

        private void Start()
        {
            if (settingNeededPointsForFinishLevel != null)
            {
                settingNeededPointsForFinishLevel(this);
            }
        }
        private void OnEnable()
        {
            Clock.OnFinishClock += FinishTime;
            PlayerTopDown.OnTakePoint += AddPoint;
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
            Clock.OnFinishClock -= FinishTime;
            PlayerTopDown.OnTakePoint -= AddPoint;
        }
        public void FinishTime(Clock clock)
        {
            if (clock == null) return;
            if (clock.minutes <= 0 && clock.seconds <= 0)
            {
                finishTime = true;
            }
            CheckFinishLevel();
        }
        public void AddPoint(InventoryPlayer inventoryPlayer)
        {
            if (inventoryPlayer == null) return;
            //Debug.Log(inventoryPlayer.currentCoin);
            if (inventoryPlayer.currentCoin >= neededPointsForFinishLevel)
            {
                neededPoints = true;
            }
            CheckFinishLevel();
        }
        public void CheckFinishLevel()
        {
            if (neededPoints && finishTime)
            {
                FinishGame();
            }
        }
        public void FinishGame()
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
            coins.enableGeneration = false;
        }
    }
}
