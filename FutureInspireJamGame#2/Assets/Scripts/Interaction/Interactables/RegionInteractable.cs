using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TNRD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class RegionInteractable : MonoBehaviour, IInteractable
    {
        [Separator("Interaction Parameters")]
        [SerializeField] private List<SerializableInterface<IReactable>> reactables;
        [SerializeField] private bool isInteractable;

        private Coroutine _interactionCoroutine;
        private bool _isInteracting;
        private bool _interactionButtonPressed;

        public bool IsInteractable => isInteractable;

        private void Awake()
        {
            _interactionButtonPressed = false;
            _isInteracting = false;
        }

        public void InteractionStart()
        {
            
            Debug.Log("Began Interaction");
            reactables.ForEach(c => c.Value?.ReactToInteractionStart());
            _isInteracting = true;
        }

        public void InteractionStop()
        {
            Debug.Log("Ended Interaction");
            reactables.ForEach(c => c.Value?.ReactToInteractionStop());
            _isInteracting = false;
        }

        public void InteractionButtonPressed()
        {
            throw new System.NotImplementedException();
        }
    }
}
