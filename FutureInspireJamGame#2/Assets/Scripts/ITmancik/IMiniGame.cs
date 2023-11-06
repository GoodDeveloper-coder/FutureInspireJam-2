using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    public interface IMiniGame
    {
        public void SetFocusLevel(float f);
        public void MiniGameStart();
        public bool MiniGameEnded();

        public MiniGameState GetMiniGameState();
    }
}
