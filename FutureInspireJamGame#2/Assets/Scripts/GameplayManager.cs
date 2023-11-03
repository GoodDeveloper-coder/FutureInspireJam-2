using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private float focusLevelDepletionFactor;

    [SerializeField] private Vector2 cameraBoundaries;

    private int knowledgePoints;

    private int currentDay;

    private int currentPeriod;

    private bool studiedLastPeriod;
    private bool studying;

    private float focusLevel;

    private enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "4:00AM";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = Camera.main.transform.position;
        camPosition.x = player.transform.position.x < -Mathf.Abs(cameraBoundaries.x) ? -Mathf.Abs(cameraBoundaries.x) : player.transform.position.x > Mathf.Abs(cameraBoundaries.x) ? Mathf.Abs(cameraBoundaries.x) : player.transform.position.x;
        camPosition.y = player.transform.position.y < -Mathf.Abs(cameraBoundaries.y) ? -Mathf.Abs(cameraBoundaries.y) : player.transform.position.y > Mathf.Abs(cameraBoundaries.y) ? Mathf.Abs(cameraBoundaries.y) : player.transform.position.y;
        Camera.main.transform.position = camPosition;
    }

    void FixedUpdate()
    {

    }

    private void NewDay()
    {
        currentDay++;
        currentPeriod = 0;
    }

    private void NextPeriod()
    {
        currentPeriod++;
        int hour = currentPeriod / 2 + 4;
        timeText.text = (hour < 10 ? "0" : "") + hour + ":" + (currentPeriod % 2 == 0 ? "00" : "30") + (hour < 12 ? "PM" : "AM");
    }
}
