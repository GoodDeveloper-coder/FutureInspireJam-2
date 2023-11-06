using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Managers
{
    public class TimeManagerScript : MonoBehaviour
    {
        //public static TimeManagerScript instance;

        public static event Action OnMinuteChanged = delegate { };
        public static event Action OnHourChanged = delegate { };
        public static event Action OnDayChanged = delegate { };

        public int Minute { get; set; }
        public int Hour { get; set; }
        //public static int Day { get; set; }

        private Days day;
        public static bool timePaused { get; set; }

        public float MinuteToRealTime = 0.5f;
        private float timer;

        // Start is called before the first frame update
        void Start()
        {
            Minute = 0;
            Hour = 16;
            //Day = 1;
            day = Days.Monday;
            timer = MinuteToRealTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (timePaused) return;

            timer -= Time.deltaTime;

            if (timer <= 0)
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
                        Hour = 16;
                        day++;
                        OnDayChanged?.Invoke();
                    }
                }

                timer = MinuteToRealTime;
            }
        }

        public void MoveUpByHalfAnHour()
        {
            int newMinute = Minute + 30;
            OnMinuteChanged?.Invoke();
            if (newMinute > 60)
            {
                Minute = 60 - newMinute;
                int newHour = Hour + 1;
                OnHourChanged?.Invoke();
                if (newHour > 24)
                {
                    Hour = 16;
                    day++;
                    OnDayChanged?.Invoke();
                }
            } else
            {
                Minute = newMinute;
            }
        }

        public void MoveUpByADay()
        {
            day++;
            Hour = 16;
            Minute = 0;

            OnMinuteChanged?.Invoke();
            OnHourChanged?.Invoke();
            OnDayChanged?.Invoke();
            Singleton.Instance.AudioManager.SetToDay();
        }
        public void PauseTime()
        {
            timePaused = true;
        }

        public void ResumeTime()
        {
            timePaused = false;
        }

        public Days GetDay()
        {
            return day;
        }
    }
}

