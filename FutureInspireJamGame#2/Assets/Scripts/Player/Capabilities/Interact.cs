using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Capabilities
{
    public class Interact : MonoBehaviour
    {
        private class InteractableData
        {
            public IInteractable interactable;
            public Transform transform;
        }

        [SerializeField] private LayerMask interactableLayerMask;

        private List<InteractableData> _currentInteractables = new List<InteractableData>();

        private bool canInteract;
        private void Awake()
        {
            canInteract = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            var interactableObject = collision.gameObject.GetComponent<IInteractable>();

            if (interactableObject == null)
            {
                Debug.LogError("Interact => OntTriggerExit2D :  Interactable Layer Object does not have a " +
                    "script implementing IInteractable interface attached.");
                return;
            }

            
            interactableObject.InteractionStart();
            if (!_currentInteractables.Exists(x => x.interactable == interactableObject))
            {
                _currentInteractables.Add(new InteractableData { interactable = interactableObject, transform = collision.transform });
            }

        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            var interactableObject = collision.gameObject.GetComponent<IInteractable>();

            if (interactableObject == null)
            {
                Debug.LogError("Interact => OntTriggerExit2D :  Interactable Layer Object does not have a " +
                    "script implementing IInteractable interface attached.");
                return;
            }

            interactableObject.InteractionStop();
            _currentInteractables.RemoveAll(x => x.interactable == interactableObject);
        }

        public void OnInteractionButtonPressed(InputAction.CallbackContext value)
        {
            if (!canInteract) return;
            if (value.canceled)
            {
                Debug.Log("Button Pressed");
                foreach(InteractableData obj in _currentInteractables)
                {
                    obj.interactable.InteractionButtonPressed();
                }
            }
        }

        public void StopInteraction ()
        {
            canInteract = false;
        }

        public void RestartInteraction()
        {
            canInteract = true;
        }

    }
}
