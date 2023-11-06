using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MiniGames
{
    public class FocusScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textFocus;

        [SerializeField] private MiniGameManager minigameManager;

        private float denominator;
        private bool plumberIsHere;

        // Start is called before the first frame update
        void Start()
        {
            denominator = 1f;
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }

        // Update is called once per frame
        void Update()
        {
            minigameManager.SetFocusLevel(GetFocusLevel());
        }

        private float GetFocusLevel()
        {
            return 1f / (plumberIsHere ? 1.25f * denominator : denominator);
        }

        public void FinishMinigame()
        {
            denominator *= 1.25f;
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }

        public void TakeBreak()
        {
            denominator /= 1.25f;
            if (denominator < 1f) denominator = 1f;
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }

        public void EatDinner(bool hadSnack)
        {
            if (hadSnack) // if player had a snack one hour or less before dinner
            {
                denominator *= 1.25f;
                textFocus.text = (int)(GetFocusLevel() * 100) + "%";
                return;
            }
            denominator /= 1.25f;
            if (denominator < 1) denominator = 1;
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }

        public void EatSnack()
        {
            denominator /= 1.25f;
            if (denominator < 1) denominator = 1;
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }

        public void NewDay(float hoursAfterBedtime, int snacksAfterBedtime)
        {
            denominator = 1 + (hoursAfterBedtime * 0.1f) + (snacksAfterBedtime * 0.05f);
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }

        public void SetPlumberIsHere(bool p)
        {
            plumberIsHere = p;
            textFocus.text = (int)(GetFocusLevel() * 100) + "%";
        }
    }
}
