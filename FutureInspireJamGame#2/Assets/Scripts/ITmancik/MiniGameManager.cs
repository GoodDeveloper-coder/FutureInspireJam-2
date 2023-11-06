using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using Interaction;
using Managers;
using MyBox;
using TMPro;
using TNRD;
using UnityEngine;

namespace MiniGames
{

    public enum MiniGameState

    {
        WAITINGFORSTART,
        PLAYING,
        PAUSED,
        ENDEDWON,
        ENDEDLOST
    }
    public class MiniGameManager:  MonoBehaviour
    {
        public static event Action OnMiniGameEnded = delegate { };
        [SerializeField] private NodeParser narrator;
        [SerializeField] private List<GameObject> gameIntros;
        [SerializeField] private List<TextMeshProUGUI> rewardText;
        [SerializeField] private TextMeshProUGUI endScreenRewardText;
        [SerializeField] private List<SerializableInterface<IMiniGame>> miniGamesList;
        [SerializeField] private DialogueGraph wantToStudy;
        [SerializeField] private DialogueGraph cantStudy;
        [SerializeField] private DialogueGraph continueStudying;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        private Coroutine checkDialogueCoroutine;
        private Coroutine checkIntroCoroutine;
        private Coroutine checkEndCoroutine;

        private int rewardLevel;
        private int nextAvailableHour;
        private int nextAvailableMinute;
        private bool skipScreen;
        private bool canPlay;
        private void OnEnable()
        {
            GoToEatScript.OnGoToEat += ResetRewardLevel;
            TimeManagerScript.OnMinuteChanged += TimeManagerScript_OnMinuteChanged;
        }

        private void TimeManagerScript_OnMinuteChanged()
        {
            int currentHour = Singleton.Instance.TimeManager.Hour;
            int currentMin = Singleton.Instance.TimeManager.Hour;

            if (currentHour == nextAvailableHour)
            {
                if (currentMin >= nextAvailableMinute)
                {
                    canPlay = true;
                }
            }
            else if (currentHour > nextAvailableHour)
            {
                canPlay = true;
            }
        }

        private void OnDisable()
        {
            GoToEatScript.OnGoToEat -= ResetRewardLevel;
            TimeManagerScript.OnMinuteChanged -= TimeManagerScript_OnMinuteChanged;
        }

        private void Awake()
        {
            foreach (var item in gameIntros)
            {
                item.SetActive(false);
            }
            rewardLevel = 5;
            skipScreen = false;
            canPlay = true;
        }

        private int currentGameIndex = 0;

        // 1
        public void ShouldStudy()
        {
            
            if (canPlay)
            {
                narrator.SetDialogueGraph(wantToStudy);
                narrator.BeginDialogue();
                if (checkDialogueCoroutine != null) StopCoroutine(checkDialogueCoroutine);
                checkDialogueCoroutine = StartCoroutine(CheckForDialogueEnd());
                Singleton.Instance.GameManager.StopPlayerInput();
            }
            else
            {
                narrator.SetDialogueGraph(cantStudy);
                narrator.BeginDialogue();
                
            }
        }
        //3
        public void ChooseRandomMiniGame()
        {
            currentGameIndex = UnityEngine.Random.Range(0, miniGamesList.Count);
            gameIntros[currentGameIndex].SetActive(true);
            rewardText[currentGameIndex].text = "Reward: " + Convert.ToString(rewardLevel) + "KP";
            skipScreen = false;
            StartCoroutine(CheckIntroSkipped());
            
        }

        // 2
        IEnumerator CheckForDialogueEnd()
        {
            Debug.Log("Waiting for dialogue End");
            yield return new WaitUntil(() => narrator.IsDoneWithDialogue());
            Singleton.Instance.GameManager.StopPlayerInput();
            Debug.Log("Dialogue End");
            if (narrator.ReturnLastAnswer())
            {
                ChooseRandomMiniGame();
            } else
            {
                Singleton.Instance.GameManager.RestartPlayerInput();
            }
        }

        // 4
        IEnumerator CheckIntroSkipped()
        {
            Debug.Log("Waiting for Intro skip");
            yield return new WaitUntil(() => skipScreen);
            Singleton.Instance.GameManager.StopPlayerInput();
            Debug.Log("Intro skip");
            skipScreen = false;
            gameIntros[currentGameIndex].SetActive(false);
            miniGamesList[currentGameIndex].Value.MiniGameStart();
            StartCoroutine(CheckGameOver());
        }

        // 5
        public IEnumerator CheckGameOver()
        {
            Debug.Log("Waiting for Game End");
            yield return new WaitUntil(() => miniGamesList[currentGameIndex].Value.MiniGameEnded());
            Debug.Log("Game End");
            //OnMiniGameEnded?.Invoke();
            if (miniGamesList[currentGameIndex].Value.GetMiniGameState() == MiniGameState.ENDEDWON)
            {
                nextAvailableHour = -1;
                winScreen.gameObject.SetActive(true);
                skipScreen = false;
                endScreenRewardText.text = "Reward: " + Convert.ToString(rewardLevel) + "KP";
                rewardLevel += 1;
                StartCoroutine(CheckEndScreensSkipped());
            }
            else if (miniGamesList[currentGameIndex].Value.GetMiniGameState() == MiniGameState.ENDEDLOST)
            {
                nextAvailableHour = Singleton.Instance.TimeManager.Hour + 1;
                nextAvailableMinute = 60 - Singleton.Instance.TimeManager.Minute;
                canPlay = false;
                loseScreen.gameObject.SetActive(true);
                skipScreen = false;
                StartCoroutine(CheckEndScreensSkipped());
            }
            
        }
        // 6
        IEnumerator CheckEndScreensSkipped()
        {
            Debug.Log("Waiting for End Skip");
            yield return new WaitUntil(() => skipScreen);
            Debug.Log("End Skip");
            skipScreen = false;
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);

            if (miniGamesList[currentGameIndex].Value.GetMiniGameState() == MiniGameState.ENDEDWON)
            {
                narrator.SetDialogueGraph(continueStudying);
                narrator.BeginDialogue();
                StartCoroutine(CheckForDialogueEnd());
            } else if (miniGamesList[currentGameIndex].Value.GetMiniGameState() == MiniGameState.ENDEDLOST)
            {
                Singleton.Instance.GameManager.RestartPlayerInput();
            }
            
            
            
        }

        public void SkipIntro()
        {
            skipScreen = true;
        }
        
        public bool GetCanPlay()
        {
            return canPlay;
        }
        

        public void ResetRewardLevel()
        {
            rewardLevel = 5;
        }
    }
}
