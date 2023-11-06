using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using Interaction;
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
        [SerializeField] private List<GameObject> gameIntros;
        [SerializeField] private List<SerializableInterface<IMiniGame>> miniGamesList;
        [SerializeField] private DialogueGraph wantToStudy;
        [SerializeField] private DialogueGraph cantStudy;
        
        private int lastLostGameMinute;
        private bool skipIntro;
        private void Awake()
        {
            foreach (var item in gameIntros)
            {
                item.SetActive(false);
            }
            skipIntro = false;
            lastLostGameMinute = -1;
        }

        private int currentGameIndex = 0;
        public void ShouldStudy()
        {

        }
        public void ChooseRandomMiniGame()
        {
            currentGameIndex = UnityEngine.Random.Range(0, miniGamesList.Count);
            gameIntros[currentGameIndex].SetActive(true);
            skipIntro = false;
            StartCoroutine(CheckIntroSkipped());
            
        }
        public void ChooseMiniGame()
        {

        }

        IEnumerator CheckIntroSkipped()
        {
            yield return new WaitUntil(() => skipIntro);
            skipIntro = false;
            gameIntros[currentGameIndex].SetActive(false);
            miniGamesList[currentGameIndex].Value.MiniGameStart();
            StartCoroutine(CheckGameOver());
        }

        public void SkipIntro()
        {
            skipIntro = true;
        }

        public IEnumerator CheckGameOver()
        {
            yield return new WaitUntil(() => miniGamesList[currentGameIndex].Value.MiniGameEnded());
            OnMiniGameEnded?.Invoke();
        }
    }
}
