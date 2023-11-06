using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;
using Managers;

namespace Dialogue
{
    public class NodeParser : MonoBehaviour
    {
        //public static event Action OnNarrationBegan = delegate { };
        public static event Action OnNarrationEnded = delegate { };
        [SerializeField] private DialogueGraph dialogue;
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private GameObject responseBox;
        [SerializeField] private List<GameObject> responseObjectList;
        
        private TextMeshProUGUI _dialogueText;
        public List<TextMeshProUGUI> _responseTexts;

        private bool _doneWithAnswers;
        private bool _doneWithDialogue;
        private void Awake()
        {
            _dialogueText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
            for (int i = 0; i < responseObjectList.Count; ++i)
            {
                _responseTexts.Add(responseObjectList[i].GetComponentInChildren<TextMeshProUGUI>());
            }
        }
        public void BeginDialogue()
        {
            Singleton.Instance.GameManager.StopPlayerInput();
            dialogueBox.SetActive(true);
            responseBox.SetActive(true);
            //_responseTexts = new List<TextMeshProUGUI>();
            
            dialogue.Restart();
            _dialogueText.text = dialogue.current.text;

            if (dialogue.current.answers.Count == 0)
            {
                _doneWithAnswers = true;
                responseBox.SetActive(false);
            }

            for (int i = 0; i < _responseTexts.Count; ++i)
            {
                if (i < dialogue.current.answers.Count)
                {
                    responseObjectList[i].SetActive(true);
                    _responseTexts[i].enabled = false;
                    _responseTexts[i].enabled = true;

                    _responseTexts[i].SetText(dialogue.current.answers[i].text);


                }
                else
                {
                    responseObjectList[i].gameObject.SetActive(false);
                }

            }

        }
        public void AnswerGiven(int answerIndex)
        {
            if (answerIndex != - 1) dialogue.AnswerQuestion(answerIndex);

            if (dialogue.current.answers.Count == 0)
            {
                _doneWithAnswers = true;
                responseBox.SetActive(false);

            }
            else
            {
                _doneWithAnswers = false;
                responseBox.SetActive(true);
            }
            
            if (dialogue.current.text.Equals(_dialogueText.text))
            {
                _doneWithDialogue = true;
                OnNarrationEnded?.Invoke();
                dialogueBox.SetActive(false);
                responseBox.SetActive(false);
            }

            _dialogueText.text = dialogue.current.text;

            for (int i = 0; i < _responseTexts.Count; ++i)
            {
                if (i < dialogue.current.answers.Count)
                {
                    responseObjectList[i].SetActive(true);
                    _responseTexts[i].text = dialogue.current.answers[i].text;
                } else
                {
                    responseObjectList[i].gameObject.SetActive(false);
                }
            }
        }
        public void NextDialogue(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                if (_doneWithAnswers)
                {
                    responseBox.SetActive(false);
                    AnswerGiven(0);
                }

                if (_doneWithDialogue)
                {
                    dialogueBox.SetActive(false);
                    OnNarrationEnded?.Invoke();
                    _doneWithAnswers = false;
                    _doneWithDialogue = false;
                    // Reset narrator state for next dialogue sequence

                }
            }
        }

        public void SetDialogueGraph(DialogueGraph dialogueGraph)
        {
            dialogue = dialogueGraph;
        }
    }
}