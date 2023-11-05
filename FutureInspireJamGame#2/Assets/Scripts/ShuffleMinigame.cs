using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleMinigame : Minigame
{
    [SerializeField] private GameObject circlePrefab;

    [SerializeField] private int circleCount;
    [SerializeField] private float minShuffleTime;
    [SerializeField] private float maxShuffleTime;
    [SerializeField] private int minShuffleCount;
    [SerializeField] private int maxShuffleCount;

    [SerializeField] private float circleDistanceFromCentre;

    [SerializeField] private float circleSize;
    
    private float shuffleTime;
    private int shuffleCount;

    private int greenCircle;

    private GameObject[] circles;
    
    private Vector3[] shufflePositionsFrom;
    private Vector3[] shufflePositionsTo;

    private int currentShuffleIndex;
    private float currentShuffleTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!playing || win || lose) return;
        if (intro)
        {
            currentShuffleTime += Time.deltaTime;
            if (currentShuffleTime < introTime) circles[greenCircle].GetComponent<SpriteRenderer>().color = Color.green + (Color.black - Color.green) * currentShuffleTime / introTime;
            else
            {
                circles[greenCircle].GetComponent<SpriteRenderer>().color = Color.black;
                intro = false;
                currentShuffleTime = 0;
            }
            return;
        }
        if (currentShuffleIndex >= shuffleCount)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (int i = 0; i < circles.Length; i++)
                {
                    if (Vector2.Distance(mousePosition, new Vector2(circles[i].transform.position.x, circles[i].transform.position.y)) < circleSize / 2)
                    {
                        if (i == greenCircle) win = true;
                        else lose = true;
                        i = circles.Length;
                    }
                }
                if (win || lose)
                {
                    foreach (GameObject c in circles) Destroy(c);
                }
            }
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
            Shuffle();
        }
    }

    public override void PlayMinigame(float focusLevel)
    {
        playing = true;
        win = false;
        lose = false;
        shuffleTime = minShuffleTime + (maxShuffleTime - minShuffleTime) * focusLevel;
        shuffleCount = (int)(maxShuffleCount - (maxShuffleCount - minShuffleCount) * focusLevel);
        currentShuffleIndex = 0;

        intro = true;
        greenCircle = Random.Range(0, circleCount);
        if (circles != null)
        {
            foreach (GameObject c in circles) Destroy(c);
        }
        circles = new GameObject[circleCount];
        shufflePositionsFrom = new Vector3[circleCount];
        shufflePositionsTo = new Vector3[circleCount];
        for (int i = 0; i < circleCount; i++)
        {
            circles[i] = Instantiate(circlePrefab, transform.position + (Vector3.right * Mathf.Sin(i * Mathf.PI * 2f / circleCount) + Vector3.up * Mathf.Cos(i * Mathf.PI * 2f / circleCount)) * circleDistanceFromCentre, transform.rotation);
            circles[i].transform.localScale = (Vector3.right + Vector3.up) * circleSize + Vector3.forward;
            circles[i].GetComponent<SpriteRenderer>().color = i == greenCircle ? Color.green : Color.black;
        }
        Shuffle();
    }

    private void Shuffle()
    {
        currentShuffleIndex++;
        currentShuffleTime = 0;
        bool stuck = false;
        do
        {
            stuck = false;
            int iterations;
            bool[] index = new bool[circleCount];
            for (int i = 0; i < circleCount; i++)
            {
                if (!stuck)
                {
                    iterations = 0;
                    shufflePositionsFrom[i] = circles[i].transform.position;
                    int r;
                    do
                    {
                        r = Random.Range(0, circleCount);
                        iterations++;
                    }
                    while (iterations < 100 && (i == r || index[r]));
                    shufflePositionsTo[i] = circles[r].transform.position;
                    index[r] = true;
                    if (iterations >= 100) stuck = true;
                }
            }
        }
        while (stuck);
    }
}
