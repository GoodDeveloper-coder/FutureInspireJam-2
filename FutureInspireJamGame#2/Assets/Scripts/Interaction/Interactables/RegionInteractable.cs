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
        [SerializeField] private bool usesInteractButton;
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
            Debug.Log("Interacted");
            if (usesInteractButton)
            {
                _isInteracting = true;
                _interactionCoroutine = StartCoroutine(InteractionRoutine());
                
            } else
            {
                reactables.ForEach(c => c.Value?.ReactToInteractionStart());
            }
        }

        public void InteractionStop()
        {
            reactables.ForEach(c => c.Value?.ReactToInteractionStop());
            if (_interactionCoroutine != null) StopCoroutine(_interactionCoroutine);
        }

        public void RegisterInteractable()
        {
            throw new System.NotImplementedException();
        }
        public void OnInteraction (InputAction.CallbackContext value)
        {
            if (value.canceled)
            {
                _interactionButtonPressed = true;
            }
        }
        IEnumerator InteractionRoutine()
        {
            while (_isInteracting && !_interactionButtonPressed)
            {
                Debug.Log("Continue Interaction");
                yield return null;

            }

            reactables.ForEach(c => c.Value?.ReactToInteractionStart());
            _interactionButtonPressed = false;
        }
    }
}
