using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class SwitchReactable : MonoBehaviour, IReactable
    {
        [SerializeField] private bool activatedAtStart;
        [SerializeField] private bool multipleActivationAllowed;
        [SerializeField] private SpriteRenderer visual;
        private bool _alreadyActivatedOnce;
        private bool _currentlyActivated;
        private void Awake()
        {
            _alreadyActivatedOnce = false;
            if (activatedAtStart)
            {
                visual.enabled = true;
                _currentlyActivated = true;
            } else
            {
                visual.enabled = false;
                _currentlyActivated = false;

            }
        }
        public void ReactToInteractionStart()
        {
            if (!multipleActivationAllowed && _alreadyActivatedOnce) return;
            visual.enabled = !visual.enabled;
            _alreadyActivatedOnce = true;
        }

        public void ReactToInteractionStop()
        {
            if (!multipleActivationAllowed && _alreadyActivatedOnce) return;
            visual.enabled = !visual.enabled;
        }
    }

}
