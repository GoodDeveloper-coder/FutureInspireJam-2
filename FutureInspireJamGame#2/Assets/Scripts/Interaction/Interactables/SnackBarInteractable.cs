using Dialogue;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class SnackBarInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private NodeParser narrator;
        [SerializeField] private DialogueGraph snackEatenRecentlyMsg;
        [SerializeField] private DialogueGraph snacksAvailableMsg;

        [SerializeField] private int initialSnackCount;
        private int _currentSnacksCount;
        public bool _snackEatenRecently;
        public bool _isInteractable;
        public bool IsInteractable => _isInteractable;

        private void OnEnable()
        {
            TimeManagerScript.OnHourChanged += TimeManager_HourChanged;
        }
        private void OnDisable()
        {
            TimeManagerScript.OnHourChanged -= TimeManager_HourChanged;
        }

        public void TimeManager_HourChanged()
        {
            _snackEatenRecently = false;
        }
        private void Awake()
        {
            _currentSnacksCount = initialSnackCount;
            _snackEatenRecently = false;
            _isInteractable = true;
        }
        public void InteractionButtonPressed()
        {
            Debug.Log("SNACK BAR");
            if (!_isInteractable) return;
            
            if (_snackEatenRecently)
            {
                narrator.SetDialogueGraph(snackEatenRecentlyMsg);
                narrator.BeginDialogue();
                //StartCoroutine(CheckForDialogueEnd());
                return;
            }

            if (_currentSnacksCount > 0){
                narrator.SetDialogueGraph(snacksAvailableMsg);
                narrator.BeginDialogue();
                StartCoroutine(CheckForDialogueEnd());
            }
            
        }
        public void ConsumeSnack()
        {
            Debug.Log("Consume Snack = " + _currentSnacksCount);
            if (_currentSnacksCount > 0)
            {
                _currentSnacksCount--;
                Debug.Log("Current Snack Count = " + _currentSnacksCount);
                _snackEatenRecently = true;
                if (_currentSnacksCount <= 0)
                {
                    _isInteractable = false;
                }
            } 
            Debug.Log(_currentSnacksCount);
        }

        public void InteractionStart()
        {
            //throw new System.NotImplementedException();
        }

        public void InteractionStop()
        {
            //throw new System.NotImplementedException();
        }

        public void SetSnackCount(int snacksCount)
        {
            _currentSnacksCount = snacksCount;
            if (_currentSnacksCount > 0)
            {
                _isInteractable = true;
            }
        }
        IEnumerator CheckForDialogueEnd()
        {
            yield return new WaitUntil(() => narrator.IsDoneWithDialogue());

            if (narrator.ReturnLastAnswer())
            {
                ConsumeSnack();
            }
        }
    }
}

