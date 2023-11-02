using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int Minute;
    public int Hour;

    public int KnowledgePoints;

    public AudioSource MainTrack;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Minute = TimeManagerScript.Minute;
        Hour = TimeManagerScript.Hour;
    }
}
