using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedDayFate : MonoBehaviour
{

    public GameObject FateIn;
    public GameObject FateOut;

    public TimeManagerScript FateOutScript;
    public int TestHour;
    public int TestMinute;

    // Start is called before the first frame update
    void Start()
    {
        TestHour = FateOutScript.Hour;
        TestMinute = FateOutScript.Minute;
    }

    // Update is called once per frame
    void Update()
    {
        TestHour = FateOutScript.Hour;
        TestMinute = FateOutScript.Minute;
        if (TestHour >= 23)
        {
            if (TestMinute >= 59) 
            {
                StartCoroutine(IDkName());
            }
        }
    }

    IEnumerator IDkName()
    {
        FateIn.SetActive(true);
        yield return new WaitForSeconds(5f);
        FateIn.SetActive(false);
        FateOut.SetActive(true);
        yield return new WaitForSeconds(5f);
        FateOut.SetActive(false);
    }
}
