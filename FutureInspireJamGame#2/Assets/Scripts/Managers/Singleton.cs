using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance { get; private set; }
        public TimeManagerScript TimeManager { get; private set; }
        public GameManagerScript GameManager { get; private set; }
        public AudioManagerScript AudioManager { get; private set; }
        //public UIManager UIManager { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            TimeManager = GetComponentInChildren<TimeManagerScript>();
            GameManager = GetComponentInChildren<GameManagerScript>();
            AudioManager = GetComponentInChildren<AudioManagerScript>();
            //UIManager = GetComponentInChildren<UIManager>();
        }
    }
}

