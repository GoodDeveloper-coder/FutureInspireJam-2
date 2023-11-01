using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public interface IReactable
    {
        public void ReactToInteractionStart();
        public void ReactToInteractionStop();

    }
}