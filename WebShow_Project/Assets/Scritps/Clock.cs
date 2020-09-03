using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    // Start is called before the first frame update
    public float minutes;
    public float seconds;
    public static event Action<Clock> OnFinishClock;
    public bool UpdateInClock = true;
    // Update is called once per frame
    void Update()
    {
        if (UpdateInClock)
        {
            UpdateClock();
        }
    }
    public void UpdateClock()
    {
        if (seconds > 0)
        {
            seconds = seconds - Time.deltaTime;
        }
        else
        {
            if (minutes > 0)
            {
                minutes--;
                seconds = 59;
            }
            else
            {
                minutes = 0;
                if (OnFinishClock != null)
                    OnFinishClock(this);
            }
        }
    }
}
