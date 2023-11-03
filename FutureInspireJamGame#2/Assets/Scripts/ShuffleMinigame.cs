using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleMinigame : Minigame
{
    [SerializeField] private GameObject circlePrefab;

    [SerializeField] private int minCircleCount;
    [SerializeField] private int maxCircleCount;
    [SerializeField] private float minShuffleTime;
    [SerializeField] private float maxShuffleTime;
    [SerializeField] private int minShuffleCount;
    [SerializeField] private int maxShuffleCount;

    [SerializeField] private float circleDistanceFromCentre;
    
    private int circleCount;
    private float shuffleTime;
    private int shuffleCount;

    private int whiteCircle;

    private GameObject[] circles;
    
    private Vector3[] shufflePositionsFrom;
    private Vector3[] shufflePositionsTo;

    private int currentShuffleIndex;
    private float currentShuffleTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!playing) return;
        if (intro)
        {
            currentShuffleTime += Time.deltaTime;
            if (currentShuffleTime < introTime)
            {
                circles[whiteCircle].transform.position = Vector3.Lerp(transform.position, shufflePositionsFrom[whiteCircle], currentShuffleTime / introTime);
                circles[whiteCircle].GetComponent<SpriteRenderer>().color = Color.green + (Color.white - Color.green) * currentShuffleTime / introTime;
            }
            else
            {
                circles[whiteCircle].transform.position = shufflePositionsFrom[whiteCircle];
                circles[whiteCircle].GetComponent<SpriteRenderer>().color = Color.white;
                intro = false;
                currentShuffleTime = 0;
            }
            return;
        }
        if (currentShuffleIndex >= shuffleCount)
        {
            // check if circle has been clicked on
            return;
        }
        currentShuffleTime += Time.deltaTime;
        if (currentShuffleTime < shuffleTime)
        {
            for (int i = 0; i < circleCount; i++) circles[i].transform.position = Vector3.Lerp(shufflePositionsFrom[i], shufflePositionsTo[i], currentShuffleTime / shuffleTime);
        }
        else
        {
            for (int i = 0; i < circleCount; i++) circles[i].transform.position = shufflePositionsTo[i];
            currentShuffleIndex++;
            currentShuffleTime = 0;
            Shuffle();
        }
    }

    public override void PlayMinigame(float focusLevel)
    {
        playing = true;
        circleCount = (int)(maxCircleCount - (maxCircleCount - minCircleCount) * focusLevel);
        shuffleTime = minShuffleTime + (maxShuffleTime - minShuffleTime) * focusLevel;
        shuffleCount = (int)(maxShuffleCount - (maxShuffleCount - minShuffleCount) * focusLevel);
        currentShuffleIndex = 0;

        intro = true;
        whiteCircle = Random.Range(0, circleCount);
        if (circles != null)
        {
            foreach (GameObject c in circles) Destroy(c);
        }
        circles = new GameObject[circleCount];
        shufflePositionsFrom = new Vector3[circleCount];
        shufflePositionsTo = new Vector3[circleCount];
        for (int i = 0; i < circleCount; i++)
        {
            circles[i] = Instantiate(circlePrefab, transform.position + new Vector3(Mathf.Sin(i * Mathf.PI * 2f / circleCount), Mathf.Cos(i * Mathf.PI * 2f / circleCount), 0) * circleDistanceFromCentre, transform.rotation);
            circles[i].transform.localScale = (Vector3.right + Vector3.up) * circleDistanceFromCentre * Mathf.Sqrt(3f / circleCount) + Vector3.forward;
            circles[i].GetComponent<SpriteRenderer>().color = i == whiteCircle ? Color.green : Color.white;
        }
        Shuffle();
    }

    private void Shuffle()
    {
        bool stuck = false;
        do
        {
            stuck = false;
            int iterations = 0;
            bool[] index = new bool[circleCount];
            for (int i = 0; i < circleCount; i++)
            {
                iterations = 0;
                shufflePositionsFrom[i] = circles[i].transform.position;
                int r;
                do r = Random.Range(0, circleCount);
                while (iterations < 100 && (i == r || index[r]));
                shufflePositionsTo[i] = circles[r].transform.position;
                index[r] = true;
                if (iterations >= 100) stuck = true;
            }
        }
        while (stuck);
    }
}
