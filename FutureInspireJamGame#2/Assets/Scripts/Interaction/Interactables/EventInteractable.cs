using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class EventInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<IReactable> events;
        [SerializeField] private bool isInteractable;
        public bool IsInteractable => isInteractable;
        
        public void InteractionStart()
        {
            for(int i = 0; i < events.Count; ++i)
            {
                
            }
        }

        public void InteractionStop()
        {
            throw new System.NotImplementedException();
        }
    }
}

