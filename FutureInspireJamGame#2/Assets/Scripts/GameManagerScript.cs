using Capabilities;
using Dialogue;
using MiniGames;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public enum Days
    {
        Monday,
        Tesuday,
        Wedndesday,
        Thursday,
        Friday
    }
    public enum GameState
    {
        Narration,
        Walking,
        Sleeping,
        MiniGame,
        Eating,
        Paused
    }

    public class GameManagerScript : MonoBehaviour
    {
        //public static GameManagerScript instance;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private List<GameEventSO> eventList;
        [SerializeField] private NodeParser narrator;

        public int Minute;
        public int Hour;

        public int snacks;

        public int KnowledgePoints;

        private Move playerMove;
        private Interact playerInteract;
        private GameState gameState;
        //private Days dayOfTheWeek;

        private int eventIndex;
        private void Awake()
        {
            //if (instance == null)
            //{
            //    instance = this;
            //} else
            //{
            //    Destroy(this);
            //}
            eventIndex = 0;
            //dayOfTheWeek = Days.Monday;

            playerMove = playerObject.GetComponent<Move>();
            playerInteract = playerObject.GetComponent<Interact>();
        }
        private void OnEnable()
        {
            RestartPlayerInput();
            NodeParser.OnNarrationEnded += NodeParser_OnNarrationEnded;
            MiniGameManager.OnMiniGameEnded += MiniGameManager_OnMiniGameEnded;
        }

        private void MiniGameManager_OnMiniGameEnded()
        {
            
            Debug.Log("MiniGame Ended");
            RestartPlayerInput();
        }

        private void NodeParser_OnNarrationEnded()
        {
            
            Debug.Log("Narration Ended");
            RestartPlayerInput();
        }

        private void OnDisable()
        {
            NodeParser.OnNarrationEnded -= NodeParser_OnNarrationEnded;
            MiniGameManager.OnMiniGameEnded -= MiniGameManager_OnMiniGameEnded;
        }


        // Update is called once per frame
        void Update()
        {
            Minute = Singleton.Instance.TimeManager.Minute;
            Hour = Singleton.Instance.TimeManager.Hour;
            if (eventIndex > eventList.Count) return;
            if (Singleton.Instance.TimeManager.GetDay() == eventList[eventIndex].day && Hour == eventList[eventIndex].hour)
            {
                narrator.SetDialogueGraph(eventList[eventIndex].dialogueGraph);
                narrator.BeginDialogue();
                //StopPlayerInput();
                Singleton.Instance.TimeManager.PauseTime();
                eventIndex++;
            }
        }
        public void StopPlayerInput()
        {
            playerMove.StopMovement();
            playerInteract.StopInteraction();
            Singleton.Instance.TimeManager.PauseTime();
        }
        public void RestartPlayerInput()
        {
            playerMove.Restartmovement();
            playerInteract.RestartInteraction();
            Singleton.Instance.TimeManager.ResumeTime();
        }
    }
}

