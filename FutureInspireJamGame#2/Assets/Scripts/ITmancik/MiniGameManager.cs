using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using TNRD;
using UnityEngine;

namespace MiniGames
{
    public class MiniGameManager:  MonoBehaviour
    {
        public static event Action OnMiniGameEnded = delegate { };
        [SerializeField] private List<SerializableInterface<IMiniGame>> miniGamesList;
        
        private int currentGameIndex = 0;
        public void ChooseRandomMiniGame()
        {
            int currentGameIndex = UnityEngine.Random.Range(0, miniGamesList.Count);
            miniGamesList[currentGameIndex].Value.MiniGameStart();
            StartCoroutine(CheckGameOver());
        }
        public void ChooseMiniGame()
        {

        }

        public void SetFocusLevel(float focus)
        {
            for (int i = 0; i < miniGamesList.Count; i++) miniGamesList[i].Value.SetFocusLevel(focus);
        }

        public IEnumerator CheckGameOver()
        {
            yield return new WaitUntil(() => miniGamesList[currentGameIndex].Value.MiniGameEnded());
            OnMiniGameEnded?.Invoke();
        }
    }
}
