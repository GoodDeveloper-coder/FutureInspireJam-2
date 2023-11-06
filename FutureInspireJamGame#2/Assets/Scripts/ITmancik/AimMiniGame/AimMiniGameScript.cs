using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGames
{

    
    public class AimMiniGameScript : MonoBehaviour, IMiniGame
    {
        //public static event Action OnMiniGameEnded = delegate { };
        public GameObject[] SpawnObjects;
        public Transform[] SpawnPositions;
        public float speed = 3;

        //public TextMeshProUGUI ScoreText;
        public GameObject minigameCanvas;
        public TextMeshProUGUI WinPointsText;
        public TextMeshProUGUI LoosePointsText;

        public static int PositivePoints;
        public static int NegativePoints;

        public int PointsToWin;
        public int PointsToLoose;

        public GameObject WinMenu;
        public GameObject LooseMenu;

        private Coroutine objectSpawnerCoroutine;
        private MiniGameState gameState;
        private Vector3 screenTopRightCoordinates;

        private bool gameOver;
        // Start is called before the first frame update
        void Start()
        {

            gameOver = false;
            minigameCanvas.SetActive(false);
            gameState = MiniGameState.WAITINGFORSTART;
        }

        // Update is called once per frame
        void Update()
        {
            if (gameState != MiniGameState.PLAYING) return;

            WinPointsText.text = $"to win:{PositivePoints}/{PointsToWin}";
            LoosePointsText.text = $"to loose:{NegativePoints}/{PointsToLoose}";
            //ScoreText.text = $"{points}";

            if (PositivePoints >= PointsToWin)
            {
                WinMenu.SetActive(true);
                if (objectSpawnerCoroutine != null) StopCoroutine(objectSpawnerCoroutine);
                //OnMiniGameEnded?.Invoke();
                gameState = MiniGameState.ENDEDWON;
                GameManagerScript.KnowledgePoints += 5;
                StartCoroutine(ExitMiniGame());
            }

            if (NegativePoints >= PointsToLoose)
            {
                LooseMenu.SetActive(true);
                //OnMiniGameEnded?.Invoke();
                if (objectSpawnerCoroutine != null) StopCoroutine(objectSpawnerCoroutine);
                gameState = MiniGameState.ENDEDLOST;
                StartCoroutine(ExitMiniGame());
            }


        }

        public void MiniGameStart ()
        {
            gameOver = false;
            minigameCanvas.SetActive(true);
            objectSpawnerCoroutine = StartCoroutine(SpawnAimObjects());
            gameState = MiniGameState.PLAYING;
        }

        IEnumerator SpawnAimObjects()
        {
            Vector3 screenBottomLeftCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            screenTopRightCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            Debug.Log(screenTopRightCoordinates);
            while (true)
            {
                int ObjRandomNumber = UnityEngine.Random.Range(0, SpawnObjects.Length);
                Vector3 randomSpawnOffset = new Vector2(UnityEngine.Random.Range(screenBottomLeftCoordinates.x + 0.2f, screenTopRightCoordinates.x - 0.2f), UnityEngine.Random.Range(screenBottomLeftCoordinates.y + 0.2f, screenTopRightCoordinates.y - 0.2f));
                yield return new WaitForSeconds(speed);
                GameObject spawnAimObject = Instantiate(SpawnObjects[ObjRandomNumber], randomSpawnOffset, Quaternion.identity);
                spawnAimObject.transform.SetParent(minigameCanvas.transform, true);
                
            }
            
            //StartCoroutine(SpawnAimObjects());
        }

        IEnumerator ExitMiniGame()
        {
            yield return new WaitForSeconds(5f);
            minigameCanvas.SetActive(false);
            LooseMenu.SetActive(false);
            WinMenu.SetActive(false);
            PositivePoints = 0;
            NegativePoints = 0;
            gameState = MiniGameState.WAITINGFORSTART;
            gameOver = true;
            Singleton.Instance.TimeManager.MoveUpByHalfAnHour();
        }

        public bool MiniGameEnded()
        {
            return gameOver;
        }

        public MiniGameState GetMiniGameState()
        {
            return gameState;
        }
    }
}

