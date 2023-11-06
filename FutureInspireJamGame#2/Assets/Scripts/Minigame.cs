using MiniGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    
    protected bool playing;
    protected bool intro;
    [SerializeField] protected float introTime;
    protected bool win;
    protected bool lose;
    public abstract void PlayMinigame(float focusLevel);

    public bool GetWin()
    {
        return win;
    }

    public bool GetLose()
    {
        return lose;
    }

    public MiniGameState GetGameState()
    {
        if (win)
        {
            Debug.Log("Win");
            return MiniGameState.ENDEDWON;
        } else
        {
            Debug.Log("Lost");
            return MiniGameState.ENDEDLOST;
        }
    }
}
