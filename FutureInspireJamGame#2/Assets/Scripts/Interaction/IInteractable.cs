using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        public void InteractionStart();
        public void InteractionStop();
        public void InteractionButtonPressed();
        public bool IsInteractable { get; }
    }
}
