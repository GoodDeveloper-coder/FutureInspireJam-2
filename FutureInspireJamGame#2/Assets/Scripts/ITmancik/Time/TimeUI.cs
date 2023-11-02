using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI TimeText;

    private void OnEnable()
    {
        TimeManagerScript.OnMinuteChanged += UpdateTime;
        TimeManagerScript.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManagerScript.OnMinuteChanged -= UpdateTime;
        TimeManagerScript.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        TimeText.text = $"{TimeManagerScript.Hour}:{TimeManagerScript.Minute:00}";
    }

}
