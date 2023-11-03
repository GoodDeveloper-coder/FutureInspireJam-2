using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    protected bool playing;
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
}
