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
        [SerializeField] private GameObject playerObject;
        [SerializeField] private List<GameEventSO> eventList;
        [SerializeField] private NodeParser narrator;

        public int Minute;
        public int Hour;
        

        public int KnowledgePoints;

        public AudioSource MainTrack;
        private Move playerMove;
        private Interact playerInteract;
        private GameState gameState;
        private Days dayOfTheWeek;

        private int eventIndex;
        private void Awake()
        {
            eventIndex = 0;
            dayOfTheWeek = Days.Monday;
            
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
            RestartPlayerInput();
        }

        private void NodeParser_OnNarrationEnded()
        {
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
            Minute = TimeManagerScript.Minute;
            Hour = TimeManagerScript.Hour;
            if (eventIndex > eventList.Count) return;
            if (dayOfTheWeek == eventList[eventIndex].day && Hour == eventList[eventIndex].hour)
            {
                narrator.SetDialogueGraph(eventList[eventIndex].dialogueGraph);
                narrator.BeginDialogue();
                StopPlayerInput();
                TimeManagerScript.timePaused = true;
                eventIndex++;
            }
        }
        public void StopPlayerInput()
        {
            playerMove.StopMovement();
            playerInteract.StopInteraction();
            TimeManagerScript.timePaused = true;
        }
        public void RestartPlayerInput()
        {
            playerMove.Restartmovement();
            playerInteract.RestartInteraction();
            TimeManagerScript.timePaused = false;
        }
    }
}

