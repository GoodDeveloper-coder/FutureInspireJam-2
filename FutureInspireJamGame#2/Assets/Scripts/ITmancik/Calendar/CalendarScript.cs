using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarScript : MonoBehaviour
{
    public GameObject Monday;
    public GameObject Tuesday;
    public GameObject Wednesday;
    public GameObject Thursday;
    public GameObject Friday;

    private int CalendarDay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalendarDay = TimeManagerScript.Day;

        if (CalendarDay == 1)
        {
            //Monday.SetActive(true);
        }

        if (CalendarDay == 2)
        {
            Monday.SetActive(true);
            //Tuesday.SetActive(true);
        }

        if (CalendarDay == 3)
        {
            Tuesday.SetActive(true);
            //Monday.SetActive(false);
            //Tuesday.SetActive(false);
            //Wednesday.SetActive(true);
        }

        if (CalendarDay == 4)
        {
            Wednesday.SetActive(true);
            //Monday.SetActive(false);
            //Tuesday.SetActive(false);
            //Wednesday.SetActive(false);
            //Thursday.SetActive(true);
        }


        if (CalendarDay == 5)
        {
            Thursday.SetActive(true);
        }
    }
}
