using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManagerScript : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    public float MinuteToRealTime = 0.5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Minute = 0;
        Hour = 10;
        timer = MinuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();

                if (Hour >= 24)
                {
                    Hour = 0;
                }
            }

            timer = MinuteToRealTime;
        }
    }
}
