using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TNRD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class ActivationInteractable : MonoBehaviour, IInteractable
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
        }

        public void InteractionStop()
        {
        }

        public void InteractionButtonPressed()
        {
            if (!_isInteracting)
            {
                Debug.Log("Began Activation Interaction");
                reactables.ForEach(c => c.Value?.ReactToInteractionStart());
                _isInteracting = true;
            } else
            {
                Debug.Log("Ended Activation Interaction");
                reactables.ForEach(c => c.Value?.ReactToInteractionStop());
                _isInteracting = false;
            }
        }
    }
}
