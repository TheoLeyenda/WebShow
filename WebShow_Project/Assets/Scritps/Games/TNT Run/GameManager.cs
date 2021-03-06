﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TNT_Run
{
    using TNT_Floor;
    using Players;
    public class GameManager : MonoBehaviour
    {
        public CoinsGenerator coins;
        public bool DisableCoins = true;
        public GameObject doorExit;
        public GameObject arrowExit;
        public TNT_Run[] tntRunObjectList;
        public List<TNT_Floor> tntFloorObjectsList;
        public PlayerTopDown[] players;
        public enum GameMode
        {
            Multiplayer,
            Time,
        }
        public enum ModeManager
        {
            DebugMode,
            NormalMode,
        }
        private void Start()
        {
            tntRunObjectList = FindObjectsOfType<TNT_Run>();
            players = FindObjectsOfType<PlayerTopDown>();
            for (int i = 0; i < tntRunObjectList.Length; i++)
            {
                tntRunObjectList[i].players = players;
            }
        }
        public GameMode gameMode = GameMode.Time;
        public ModeManager modeManager;
        private void OnEnable()
        {
            if (gameMode == GameMode.Time)
            {
                Clock.OnFinishClock += FinishGame;
            }    
        }
        private void OnDisable()
        {
            if (gameMode == GameMode.Time)
            {
                Clock.OnFinishClock -= FinishGame;
            }
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            CheckDebugMode();
        }
        public void CheckDebugMode()
        {
            if (modeManager == ModeManager.DebugMode)
            {
                if (Input.GetKey(KeyCode.E))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                }
            }
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
            for (int i = 0; i < tntRunObjectList.Length; i++)
            {
                if (tntRunObjectList[i] != null)
                {
                    tntRunObjectList[i].stateTNT = TNT_Run.StateTNT.Normal;
                    tntRunObjectList[i].isMortal = false;
                    tntRunObjectList[i].enableUpdate = false;
                }
            }
            for (int i = 0; i < tntFloorObjectsList.Count; i++)
            {
                if (tntFloorObjectsList[i] != null)
                {
                    tntFloorObjectsList[i].stateTNT = TNT_Floor.StateTNT.Normal;
                    tntFloorObjectsList[i].enableUpdate = false;
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
