using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

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

        private bool _interactionActive;

        private void Awake()
        {
            _interactionActive = true;
        }
        public void InitiateInteractionEvent()
        {
            throw new System.NotImplementedException();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Colliding");
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

            Debug.Log("Start interacting");
            interactableObject.InteractionStart();
            if (!_currentInteractables.Exists(x => x.interactable == interactableObject))
            {
                _currentInteractables.Add(new InteractableData { interactable = interactableObject, transform = collision.transform });
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Stopped Interacting");
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

    }
}
