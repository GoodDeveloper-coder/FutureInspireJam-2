using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class SwitchReactable : MonoBehaviour, IReactable
    {
        [SerializeField] private bool activatedAtStart;
        [SerializeField] private bool multipleActivationAllowed;

        private bool _alreadyActivatedOnce;
        private bool _currentlyActivated;
        private void Awake()
        {
            _alreadyActivatedOnce = false;
            _currentlyActivated = activatedAtStart;
            gameObject.SetActive(_currentlyActivated);
        }
        public void ReactToInteractionStart()
        {
            if (_alreadyActivatedOnce && !multipleActivationAllowed) return;
            _alreadyActivatedOnce = true;
            _currentlyActivated = true;
            gameObject.SetActive(_currentlyActivated);
        }

        public void ReactToInteractionStop()
        {
            if (_alreadyActivatedOnce && !multipleActivationAllowed) return;
            _currentlyActivated = false;
            gameObject.SetActive(_currentlyActivated);
        }
    }

}
