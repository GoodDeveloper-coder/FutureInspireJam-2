using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    public class GeneralMinigameScript : MonoBehaviour, IMiniGame
    {
        private Minigame minigame;

        // Start is called before the first frame update
        void Start()
        {
            minigame = GetComponent<Minigame>();
            //minigame.PlayMinigame(0.5f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MiniGameStart()
        {
            minigame.PlayMinigame(0.5f);
        }

        public bool MiniGameEnded()
        {
            return minigame.GetWin() || minigame.GetLose();
        }

        public MiniGameState GetMiniGameState()
        {
            return minigame.GetGameState();
        }
    }
}
