using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleMinigame : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;

    [SerializeField] private int initialCircleCount;
    [SerializeField] private float initialShuffleSpeed;
    [SerializeField] private float shuffleSpeedIncrementFactor;

    [SerializeField] private float circleDistanceFromCentre;

    private GameObject[] circles;

    private int shuffleCircle;
    private Vector3 shufflePositionFrom;
    private Vector3 shufflePositionTo;

    // Start is called before the first frame update
    void Start()
    {
        circles = new GameObject[initialCircleCount];
        shufflePositionFrom = new Vector3();
        shufflePositionTo = new Vector3();
        for (int i = 0; i < initialCircleCount + 1; i++)
        {
            Vector3 position = transform.position + new Vector3(Mathf.Sin(i * Mathf.PI * 2f / (initialCircleCount + 1)), Mathf.Cos(i * Mathf.PI * 2f / (initialCircleCount + 1)), 0) * circleDistanceFromCentre;
            if (i == 0) shufflePositionTo = position;
            else circles[i - 1] = Instantiate(circlePrefab, position, transform.rotation);
        }
        shuffleCircle = Random.Range(0, initialCircleCount);
        shufflePositionFrom = circles[shuffleCircle].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        circles[shuffleCircle].transform.position = Vector3.MoveTowards(circles[shuffleCircle].transform.position, shufflePositionTo, initialShuffleSpeed * Time.deltaTime);
        if (Vector3.Distance(circles[shuffleCircle].transform.position, shufflePositionTo) <= 0.1f)
        {
            int r;
            do r = Random.Range(0, initialCircleCount);
            while (shuffleCircle == r);
            shuffleCircle = r;
            shufflePositionTo = shufflePositionFrom;
            shufflePositionFrom = circles[shuffleCircle].transform.position;
        }
    }
}
