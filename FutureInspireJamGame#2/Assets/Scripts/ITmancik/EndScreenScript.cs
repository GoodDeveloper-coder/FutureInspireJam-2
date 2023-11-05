using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    public GameObject FateIn;
    public GameObject EndScreenMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManagerScript.Day >= 5)
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
