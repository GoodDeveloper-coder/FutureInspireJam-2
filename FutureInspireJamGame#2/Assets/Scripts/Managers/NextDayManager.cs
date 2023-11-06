using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class NextDayManager : MonoBehaviour
    {
        public List<GameEventSO> dayChangeSOs;
        public GameObject FateIn;
        public GameObject FateOut;

        private void OnEnable()
        {
        }
        private void OnDisable()
        {
            
        }
    }
}

