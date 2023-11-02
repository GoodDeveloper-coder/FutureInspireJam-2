using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private float focusLevelDepletionFactor;

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
