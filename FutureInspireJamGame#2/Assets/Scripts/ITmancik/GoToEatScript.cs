using Managers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoToEatScript : MonoBehaviour
{
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
        if (CanMakeFateIn) FateIn.SetActive(true); CanMakeFateIn = false;
        yield return new WaitForSeconds(5f);
        if (CanAddTime)
        {
            FateOut.SetActive(true);
            FateIn.SetActive(false);
            Singleton.Instance.TimeManager.Hour += 1;
            CanAddTime = false;
            if (ArrayIndex < 5)
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
        yield return new WaitForSeconds(8f);
        //FateIn.SetActive(false);
        FateOut.SetActive(false);
        CanAddTime = true;
        CanMakeFateIn = true;
    }

    void RandomTimeToGoToEat()
    {
        RandomNumber = Random.Range(1, 3);

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
