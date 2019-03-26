using System;
using System.Collections;
using System.Collections.Generic;

using Timers;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Monetization;

namespace GameCore
{
    public class UnityAdsShow : MonoBehaviour
    {
        [SerializeField] float timeFromReady = 15f;
        [SerializeField] UnityEvent adsStartShowing;
        [SerializeField] UnityEvent adsStopShowing;
        [SerializeField] UnityEvent adsError;
        [SerializeField] UnityEventString adsFinished;
        [SerializeField] UnityEventString adsSkipped;
        [SerializeField] UnityEventString adsFailed;
        [SerializeField] UnityEventBool VideoShowing;
        [SerializeField] StringVariable gameID;
        [SerializeField] bool debug;
#if UNITY_EDITOR
        private void OnValidate ()
        {
            gameID = Tools.ValidateSO<StringVariable> ("Assets/Resources/Data/Variables/UnitAds/GameID.asset");
        }
#endif
        Dictionary<ShowResult, Action<string>> eventSelector => new Dictionary<ShowResult, Action<string>> ()
        { { ShowResult.Finished, adsFinished.Invoke }, { ShowResult.Skipped, adsSkipped.Invoke }, { ShowResult.Failed, adsFailed.Invoke }
        };

        private void InititalizeIfNeed ()
        {
            if (!Monetization.isInitialized)
                Monetization.Initialize (gameID.Value, false);
        }

        public void ShowAds (Placement placement)
        {
            ShowAds (placement?placement.name: "INTERSTITIAL");
        }
        public void ShowAds (string placementId)
        {
            InititalizeIfNeed ();
            adsStartShowing.Invoke ();
            VideoShowing.Invoke (true);
            if (debug) Debug.Log ($"adsStartShowing {placementId} {name}");
            TimersManager.SetTimer (this, timeFromReady, onAdsTimeIsExpiried);
            StartCoroutine (WaitForAd (placementId));
        }

        private void onAdsTimeIsExpiried ()
        {
            if (debug) Debug.Log ($"onAdsTimeIsExpiried {name}");
            adsError.Invoke ();
            adsStopShowing.Invoke ();
            VideoShowing.Invoke (false);
        }

        IEnumerator WaitForAd (string placementId)
        {
            while (!Monetization.IsReady (placementId))
                yield return null;
            // if ready ads from placementId
            if (debug) Debug.Log ($"Ads Ready {placementId} {name}");
            TimersManager.ClearTimer (onAdsTimeIsExpiried);

            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent (placementId) as ShowAdPlacementContent;
            if (debug) Debug.Log ($"ad = {ad} {placementId} {name}");

            if (ad != null)
            {
                ad.Show (result =>
                {
                    if (debug) Debug.Log ($"Ads Show {placementId}", this);
                    eventSelector[result] (placementId);
                });
            }
            else
            {
                if (debug) Debug.Log ($"Asd Error {placementId} {name}");
                adsError.Invoke ();
            }
            if (debug) Debug.Log ($"Ads stop showing {placementId} {name}");
            adsStopShowing.Invoke ();
            VideoShowing.Invoke (false);
        }
    }
}
