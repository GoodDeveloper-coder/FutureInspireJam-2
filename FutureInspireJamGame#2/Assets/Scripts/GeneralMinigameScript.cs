using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMinigameScript : MonoBehaviour
{
    private Minigame minigame;

    // Start is called before the first frame update
    void Start()
    {
        minigame = GetComponent<Minigame>();
        minigame.PlayMinigame(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
