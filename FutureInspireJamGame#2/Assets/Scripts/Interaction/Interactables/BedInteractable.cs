using Dialogue;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class BedInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private NodeParser narrator;
        [SerializeField] private DialogueGraph notBedtimeYet;
        [SerializeField] private DialogueGraph goingToBed;
        [SerializeField] private bool isInteractable;
        
        public bool IsInteractable => throw new System.NotImplementedException();

        public void InteractionButtonPressed()
        {
            if (!isInteractable) return;
            if (Singleton.Instance.TimeManager.Hour < 21)
            {
                narrator.SetDialogueGraph(notBedtimeYet);
                narrator.BeginDialogue();
            } else
            {
                narrator.SetDialogueGraph(goingToBed);
                narrator.BeginDialogue();
            }
        }

        public void GoToSleep()
        {
            Debug.Log("Going To Sleep!");
            Singleton.Instance.TimeManager.MoveUpByADay();
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

