using Dialogue;
using Managers;
using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction {
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private NodeParser narrator;
        [SerializeField] private DialogueGraph cantGoOut;
        [SerializeField] private DialogueGraph canGoOut;
        [SerializeField] private MiniGameManager miniGameManager;
        public bool IsInteractable => throw new System.NotImplementedException();

        public void InteractionButtonPressed()
        {
            if (miniGameManager.GetCanPlay())
            {
                narrator.SetDialogueGraph(cantGoOut);
                narrator.BeginDialogue();
                // Add Fade In And out here

                Singleton.Instance.TimeManager.MoveUpByHalfAnHour();
            } else
            {
                narrator.SetDialogueGraph(canGoOut);
                narrator.BeginDialogue();
            }
}

        public void InteractionStart()
        {
            //throw new System.NotImplementedException();
        }

        public void InteractionStop()
        {
            //throw new System.NotImplementedException();
        }

       
    }
}

