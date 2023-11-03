using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICalendar : MonoBehaviour
{
    public GameObject Calendar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCalendar()
    {
        Calendar.transform.localScale = new Vector3(1, 1, 1);
    }
    public void CloseCalendar()
    {
        Calendar.transform.localScale = new Vector3(0, 0, 1);
    }
}
