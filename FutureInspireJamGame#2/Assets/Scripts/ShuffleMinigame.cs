using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleMinigame : Minigame
{
    [SerializeField] private GameObject circlePrefab;

    [SerializeField] private int initialCircleCount;
    [SerializeField] private int circleCountIncrement;
    [SerializeField] private float initialShuffleTime;
    [SerializeField] private float shuffleTimeDecrementFactor;
    [SerializeField] private int initialShuffleCount;
    [SerializeField] private int shuffleCountIncrement;

    [SerializeField] private float circleDistanceFromCentre;

    [SerializeField] private float introTime;

    private bool intro;

    private int whiteCircle;

    private GameObject[] circles;

    private int[] shuffleIndices;
    private Vector3[] shufflePositionsFrom;
    private Vector3[] shufflePositionsTo;

    private int currentShuffleIndex;
    private float currentShuffleTime;

    // Start is called before the first frame update
    void Start()
    {
        intro = true;
        whiteCircle = Random.Range(0, initialCircleCount);
        circles = new GameObject[initialCircleCount];
        shuffleIndices = new int[initialCircleCount];
        shufflePositionsFrom = new Vector3[initialCircleCount];
        shufflePositionsTo = new Vector3[initialCircleCount];
        bool[] index = new bool[initialCircleCount];
        for (int i = 0; i < initialCircleCount; i++)
        {
            circles[i] = Instantiate(circlePrefab, transform.position + new Vector3(Mathf.Sin(i * Mathf.PI * 2f / initialCircleCount), Mathf.Cos(i * Mathf.PI * 2f / initialCircleCount), 0) * circleDistanceFromCentre, transform.rotation);
            circles[i].transform.localScale = (Vector3.right + Vector3.up) * circleDistanceFromCentre * Mathf.Sqrt(3f / initialCircleCount) + Vector3.forward;
            circles[i].GetComponent<SpriteRenderer>().color = i == whiteCircle ? Color.white : Color.black;
            do shuffleIndices[i] = Random.Range(0, initialCircleCount);
            while (index[shuffleIndices[i]]);
            index[shuffleIndices[i]] = true;
        }
        for (int i = 0; i < initialCircleCount; i++)
        {
            shufflePositionsFrom[i] = circles[shuffleIndices[i]].transform.position;
            shufflePositionsTo[i] = circles[shuffleIndices[(i + 1) % initialCircleCount]].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (intro)
        {
            currentShuffleTime += Time.deltaTime;
            if (currentShuffleTime < introTime)
            {
                circles[whiteCircle].transform.position = Vector3.Lerp(transform.position, shufflePositionsFrom[shuffleIndices[whiteCircle]], currentShuffleTime / introTime);
                circles[whiteCircle].GetComponent<SpriteRenderer>().color = Color.white + (Color.black - Color.white) * currentShuffleTime / introTime;
            }
            else
            {
                circles[whiteCircle].transform.position = shufflePositionsFrom[shuffleIndices[0]];
                circles[whiteCircle].GetComponent<SpriteRenderer>().color = Color.white + (Color.black - Color.white) * currentShuffleTime / introTime;
                intro = false;
            }
            return;
        }
        if (currentShuffleIndex >= initialShuffleCount) return;
        currentShuffleTime += Time.deltaTime;
        if (currentShuffleTime < initialShuffleTime)
        {
            for (int i = 0; i < initialCircleCount; i++) circles[i].transform.position = Vector3.Lerp(shufflePositionsFrom[i], shufflePositionsTo[i], currentShuffleTime / initialShuffleTime);
        }
        else
        {
            currentShuffleIndex++;
            currentShuffleTime = 0;
            bool[] index = new bool[initialCircleCount];
            for (int i = 0; i < initialCircleCount; i++)
            {
                circles[i].transform.position = shufflePositionsTo[i];
                do shuffleIndices[i] = Random.Range(0, initialCircleCount);
                while (index[shuffleIndices[i]]);
                index[shuffleIndices[i]] = true;
            }
            for (int i = 0; i < initialCircleCount; i++)
            {
                shufflePositionsFrom[i] = circles[shuffleIndices[i]].transform.position;
                shufflePositionsTo[i] = circles[shuffleIndices[(i + 1) % initialCircleCount]].transform.position;
            }
        }
    }

    public override void PlayMinigame(float focusLevel)
    {
        playing = true;
    }
}
