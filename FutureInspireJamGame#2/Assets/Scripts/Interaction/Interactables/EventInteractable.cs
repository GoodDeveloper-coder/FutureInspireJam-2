using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class EventInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<UnityEvent> events;
        //[SerializeField] private bool isInteractable;
        public bool IsInteractable => isInteractable;
        private bool isInteractable;

        private void Awake()
        {
            isInteractable = true;
        }
        public void InteractionButtonPressed()
        {
            if (!isInteractable) return;
            for (int i = 0; i < events.Count; ++i)
            {
                Debug.Log(i);
                events[i]?.Invoke();
            }
            isInteractable = false;
        }

        public void InteractionStart()
        {
            
        }

        public void InteractionStop()
        {
            //throw new System.NotImplementedException();
        }
    }
}

