using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoToEatScript : MonoBehaviour
{
    public static event Action OnGoToEat = delegate { };
    public GameEventSO[] EatTime;

    public int ArrayIndex = 0;
    public int RandomNumber;
    //public int TestArrayIndex = 0;

    public GameObject FateIn;
    public GameObject FateOut;

    bool CanAddTime = true;
    bool CanMakeFateIn = true;


    // Start is called before the first frame update
    void Start()
    {
        RandomTimeToGoToEat();
    }

    // Update is called once per frame
    void Update()
    {
       if (EatTime[ArrayIndex].hour == Singleton.Instance.TimeManager.Hour) {
            StartCoroutine(ff());
       } 
    }

    IEnumerator ff()
    {
        yield return new WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);
        if (CanMakeFateIn)
        {
            FateIn.SetActive(true); CanMakeFateIn = false;
            Debug.Log("Fading out?");
            OnGoToEat?.Invoke();
        }
        yield return new WaitForSeconds(5f);
        if (CanAddTime)
        {
            FateOut.SetActive(true);
            FateIn.SetActive(false);
            Singleton.Instance.TimeManager.Hour += 1;
            CanAddTime = false;
            if (ArrayIndex < 4)
            {
                ArrayIndex++;
                RandomTimeToGoToEat();
            }
            else
            {

            }

            /*
            TestArrayIndex++;
            if (EatTime[TestArrayIndex] == null) {
                //ArrayIndex--;
            }
            else
            {
                ArrayIndex++;
            }
            */
        }
        yield return new WaitForSeconds(6f);
        //FateIn.SetActive(false);
        FateOut.SetActive(false);
        CanAddTime = true;
        CanMakeFateIn = true;
    }

    void RandomTimeToGoToEat()
    {
        RandomNumber = UnityEngine.Random.Range(1, 3);
        bool OnlyForFirstDay = true;

        if (OnlyForFirstDay)
        {
            RandomNumber = 2;
            OnlyForFirstDay = false;
        }
        if (RandomNumber == 1)
        {
            EatTime[ArrayIndex].hour = 17;
        }

        if (RandomNumber == 2)
        {
            EatTime[ArrayIndex].hour = 18;
        }

    }
}
