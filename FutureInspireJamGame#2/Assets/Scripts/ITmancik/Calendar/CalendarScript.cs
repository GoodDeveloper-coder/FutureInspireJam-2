using Managers;
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

    private Days CalendarDay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalendarDay = Singleton.Instance.TimeManager.GetDay();

        if (CalendarDay == Days.Monday)
        {
            //Monday.SetActive(true);
        }

        if (CalendarDay == Days.Tesuday)
        {
            Monday.SetActive(true);
            //Tuesday.SetActive(true);
        }

        if (CalendarDay == Days.Wedndesday)
        {
            Tuesday.SetActive(true);
            //Monday.SetActive(false);
            //Tuesday.SetActive(false);
            //Wednesday.SetActive(true);
        }

        if (CalendarDay == Days.Thursday)
        {
            Wednesday.SetActive(true);
            //Monday.SetActive(false);
            //Tuesday.SetActive(false);
            //Wednesday.SetActive(false);
            //Thursday.SetActive(true);
        }


        if (CalendarDay == Days.Friday)
        {
            Thursday.SetActive(true);
        }
    }
}
