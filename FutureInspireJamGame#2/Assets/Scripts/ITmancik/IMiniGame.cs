using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    public interface IMiniGame
    {
        public void MiniGameStart();
        public bool MiniGameEnded();
    }
}
