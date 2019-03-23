using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Monetization;

namespace GameCore
{
    public class PlacementIsReady : MonoBehaviour
    {
        [SerializeField] Placement placement;
        [Range (1, 15)]
        [SerializeField] float intervalCheckUpdate = 5f;
        [SerializeField] bool startUpdateOnEnable;

        [SerializeField] UnityEventBool onIsReady;
        [SerializeField] UnityEventBool onIsReadyInverce;
        [SerializeField] UnityEvent onReady;
        [SerializeField] UnityEvent onNOTReady;

        [SerializeField] StringVariable gameID;
#if UNITY_EDITOR
        private void OnValidate ()
        {
            gameID = Tools.ValidateSO<StringVariable> ("Assets/Resources/Data/Variables/UnitAds/GameID.asset");
        }
#endif
        private void OnEnable ()
        {
            if (startUpdateOnEnable)
            {
                StartCoroutine (cr_Check ());
            }
        }

        IEnumerator cr_Check ()
        {
            WaitForSeconds wfs = new WaitForSeconds (intervalCheckUpdate);
            while (true)
            {
                Check ();
                yield return wfs;
            }
        }

        bool isReady => Monetization.IsReady (placement?placement.name: "INTERSITIAL");
        private void InititalizeIfNeed ()
        {
            if (!Monetization.isInitialized)
                Monetization.Initialize (gameID.Value, false);
        }
        public void Check ()
        {
            InititalizeIfNeed ();
            onIsReady.Invoke (isReady);
            onIsReadyInverce.Invoke (!isReady);
            (isReady?(Action) onReady.Invoke : onNOTReady.Invoke) ();
        }
        private void OnDisable ()
        {
            StopAllCoroutines ();
        }
    }
}
