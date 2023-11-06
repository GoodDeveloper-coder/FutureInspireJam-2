using Managers;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    public GameObject FateIn;
    public GameObject EndScreenMenu;

    public TextMeshProUGUI FinalPointsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FinalPointsText.text = $"Knowledge points:{GameManagerScript.KnowledgePoints}";
        if (Singleton.Instance.TimeManager.GetDay() >= Days.Friday)
        {
            StartCoroutine(EndScreen());
        }
    }
    IEnumerator EndScreen()
    {
        FateIn.SetActive(true);
        yield return new WaitForSeconds(5f);
        EndScreenMenu.SetActive(true);
    }
}
