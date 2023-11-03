using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniGames
{
    public class AimMiniGameObjScript : MonoBehaviour, IPointerDownHandler
    {


        private AimMiniGameScript MiniGameScript;

        public float DestroyTime = 2f;

        public bool PositiveObject;
        public bool NegativePoints;

        // Start is called before the first frame update
        void Start()
        {
            MiniGameScript = GetComponentInParent<AimMiniGameScript>();
            StartCoroutine(DestroyAimObject());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (PositiveObject)
            {
                AimMiniGameScript.PositivePoints++;
            }

            if (NegativePoints)
            {
                AimMiniGameScript.NegativePoints++;
            }

            //MiniGameScript.points += 20;
            //Debug.Log(MiniGameScript.points);
            Destroy(this.gameObject);
        }

        IEnumerator DestroyAimObject()
        {
            yield return new WaitForSeconds(DestroyTime);
            Destroy(this.gameObject);
        }
    }
}


