using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTFloorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ModeManager
    {
        DebugMode,
        NormalMode,
    }
    public List<TNT_Floor> tnt_FloorList;
    private List<TNT_Floor> tnt_ExplotedList;
    public float maxDelayDetonateRandomTNT;
    public float minDelayDetonateRandomTNT;
    private float delay;
    public ModeManager modeManager;
    void Start()
    {
        TNT_Floor[] gameObjects = FindObjectsOfType<TNT_Floor>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            tnt_FloorList.Add(gameObjects[i]);
        }

        tnt_ExplotedList = new List<TNT_Floor>();
        delay = Random.Range(minDelayDetonateRandomTNT, maxDelayDetonateRandomTNT);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDelayActivatedTNT();
        ChechDebugMode();
    }
    public void ChechDebugMode()
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
    public void CheckDelayActivatedTNT()
    {
        if (delay > 0)
        {
            delay = delay - Time.deltaTime;
        }
        else if (delay <= 0)
        {
            delay = Random.Range(minDelayDetonateRandomTNT, maxDelayDetonateRandomTNT);
            DetonateRandomTNT();
        }
    }
    void DetonateRandomTNT()
    {
        if (tnt_FloorList.Count > 0)
        {
            int index = Random.Range(0, tnt_FloorList.Count);

            tnt_FloorList[index].ActivatedDelayDetonate();
            TNT_Floor tnt_floor = tnt_FloorList[index];
            tnt_FloorList.Remove(tnt_FloorList[index]);
            tnt_ExplotedList.Add(tnt_floor);
        }
    }
}
