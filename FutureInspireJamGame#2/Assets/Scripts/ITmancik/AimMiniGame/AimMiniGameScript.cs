using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimMiniGameScript : MonoBehaviour
{
    public GameObject[] SpawnObjects;
    public Transform[] SpawnPositions;
    public float speed = 3;

    //public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI WinPointsText;
    public TextMeshProUGUI LoosePointsText;

    //public float distance;
    //public LayerMask layerMask;

    //public int points;

    public static int PositivePoints;
    public static int NegativePoints;

    public int PointsToWin;
    public int PointsToLoose;

    public GameObject WinMenu;
    public GameObject LooseMenu;

    //public GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAimObjects());
    }

    // Update is called once per frame
    void Update()
    {
        WinPointsText.text = $"to win:{PositivePoints}/{PointsToWin}";
        LoosePointsText.text = $"to loose:{NegativePoints}/{PointsToLoose}";
        //ScoreText.text = $"{points}";

        if (PositivePoints >= PointsToWin)
        {
            WinMenu.SetActive(true);
        }

        if (NegativePoints >= PointsToLoose)
        {
            LooseMenu.SetActive(true);
        }

        /*
        Vector2 df = Camera.ScreenToWorldPoint(Mouse.current.position);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D other = Physics2D.Raycast(df, transform.up, distance, layerMask);

            if (other.collider != null)
            {
                if (other.collider.CompareTag("AimObject"))
                {
                    Destroy(other.collider.gameObject);
                    points++;
                    Debug.Log(points);
                }
            }
        }
        */
    }

    IEnumerator SpawnAimObjects()
    {
        int ObjRandomNumber = Random.Range(0, SpawnObjects.Length);
        int SpawnPosRandomNumber = Random.Range(0, SpawnPositions.Length);
        yield return new WaitForSeconds(speed);
        Instantiate(SpawnObjects[ObjRandomNumber], SpawnPositions[SpawnPosRandomNumber].position, Quaternion.identity);
        StartCoroutine(SpawnAimObjects());
    }
}
