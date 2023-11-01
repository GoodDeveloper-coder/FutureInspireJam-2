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

    public TextMeshProUGUI ScoreText;

    //public float distance;
    //public LayerMask layerMask;

    public int points;

    //public GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAimObjects());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"{points}";

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
