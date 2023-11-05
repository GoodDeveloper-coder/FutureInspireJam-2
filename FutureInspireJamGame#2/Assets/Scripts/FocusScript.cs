using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FocusScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI focusText;
    
    private float denominator;
    private bool plumberIsHere;

    // Start is called before the first frame update
    void Start()
    {
        denominator = 1;
    }

    // Update is called once per frame
    void Update()
    {
        focusText.text = (int)(GetFocusLevel() * 100) + "";
    }

    public float GetFocusLevel()
    {
        return 1f / (plumberIsHere ? denominator * 1.25f : denominator);
    }

    public void FinishMinigame()
    {
        denominator *= 1.25f;
    }

    public void TakeBreak()
    {
        denominator /= 1.25f;
        if (denominator < 1) denominator = 1;
    }

    public void EatDinner(bool hadSnack)
    {
        if (hadSnack) // if player had snack an hour or less before dinner
        {
            denominator *= 1.25f;
            return;
        }
        denominator /= 1.5f;
        if (denominator < 1) denominator = 1;
    }

    public void EatSnack()
    {
        denominator /= 1.25f;
        if (denominator < 1) denominator = 1;
    }

    public void NextDay(float hoursUpAfterBedtime, int snacksEatenAfterBedtime)
    {
        denominator = 1 + (hoursUpAfterBedtime * 0.1f) + (snacksEatenAfterBedtime * 0.05f);
    }

    public void SetPlumberIsHere(bool p)
    {
        plumberIsHere = p;
    }
}
