using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Dialogue
{
    public class NodeParser : MonoBehaviour
    {
        [SerializeField] private DialogueGraph dialogue;
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private GameObject responseBox;
        [SerializeField] private List<GameObject> responseObjectList;
        
        private TextMeshProUGUI _dialogueText;
        private List<TextMeshProUGUI> _responseTexts;

        private bool _doneWithDialogue;
        private void Awake()
        {
            _responseTexts = new List<TextMeshProUGUI>();
            _doneWithDialogue = false;
        }
        private void Start()
        {
            _dialogueText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
            for (int i = 0; i < responseObjectList.Count; ++i)
            {
                _responseTexts.Add(responseObjectList[i].GetComponentInChildren<TextMeshProUGUI>());
            }

            dialogue.Restart();
            _dialogueText.text = dialogue.current.text;
            
            for(int i = 0; i < _responseTexts.Count; ++i)
            {
                if (i < dialogue.current.answers.Count)
                {
                    _responseTexts[i].text = dialogue.current.answers[i].text;
                }
                else
                {
                    _responseTexts[i].gameObject.SetActive(false);
                }
                
            }
        }
        public void AnswerGiven(int answerIndex)
        {
            dialogue.AnswerQuestion(answerIndex);

            if (dialogue.current.answers.Count == 0)
            {
                _doneWithDialogue = true;
            }
            
            if (dialogue.current.text.Equals(_dialogueText.text))
            {
                dialogueBox.SetActive(false);
                responseBox.SetActive(false);
            }

            _dialogueText.text = dialogue.current.text;

            for (int i = 0; i < _responseTexts.Count; ++i)
            {
                if (i < dialogue.current.answers.Count)
                {
                    _responseTexts[i].text = dialogue.current.answers[i].text;
                } else
                {
                    responseObjectList[i].gameObject.SetActive(false);
                }
            }
        }
        public void ExitDialogue(InputAction.CallbackContext context)
        {
            if (context.canceled && _doneWithDialogue)
            {
                dialogueBox.SetActive(false);
                responseBox.SetActive(false);
            }
        }
    }
}