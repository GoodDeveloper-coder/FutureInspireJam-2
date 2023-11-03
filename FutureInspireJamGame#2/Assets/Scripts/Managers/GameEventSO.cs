using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName ="Game Event", fileName = "Game Events/Create New Game Event")]
    public class GameEventSO : ScriptableObject
    {
        public Days day;
        public int hour;
        public DialogueGraph dialogueGraph;
    }
}

