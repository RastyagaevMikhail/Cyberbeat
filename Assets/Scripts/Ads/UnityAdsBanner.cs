using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Advertisements;

namespace GameCore
{
    public class UnityAdsBanner : MonoBehaviour
    {
        [SerializeField] bool showOnEnable = true;
        [SerializeField] Placement placement;
        string placementId => placement.name;
        private void OnEnable ()
        {
            if (showOnEnable)
            {
                if (debug) Debug.Log ($"Banner OnEnabel {placementId} {name}");
                ShowAd ();
            }
        }

        [SerializeField] StringVariable gameID;
        [SerializeField] BoolVariable noAds;
        [SerializeField] bool debug;
        bool NoAds => noAds.Value;
#if UNITY_EDITOR
        private void OnValidate ()
        {
            gameID = Tools.ValidateSO<StringVariable> ("Assets/Resources/Data/Variables/UnitAds/GameID.asset");
            noAds = Tools.ValidateSO<BoolVariable> ("Assets/Data/Variables/AdsController/NoAds.asset");
            if (!placement) placement = Tools.ValidateSO<Placement> ("Assets/Data/Enums/Placement/BANNER.asset");
        }
#endif

        private void InititalizeIfNeed ()
        {
            if (NoAds) return;
            if (!Advertisement.isInitialized)
            {
                Advertisement.Initialize (gameID.Value, false);
                StartCoroutine (LoadBanner ());
            }
        }
        public void ShowAd ()
        {
            InititalizeIfNeed ();
            if (debug) Debug.Log ($"Banner onShow NoAds {NoAds} {placementId} {name}");
            if (NoAds) return;
            StartCoroutine (ShowBannerWhenReady (placementId));
        }
        IEnumerator ShowBannerWhenReady (string placementId)
        {
            while (!Advertisement.IsReady (placementId))
                yield return new WaitForSeconds (0.5f);
            if (debug) Debug.Log ($"Banner Ready {placementId} {name}");
            Advertisement.Banner.Show (placementId);
        }
        private void OnDisable ()
        {
            Hide ();
        }

        public void Hide ()
        {
            if (debug) Debug.Log ($"Banner onHide NoAds {NoAds} {placementId} {name}");
            if (NoAds) return;
            if (debug) Debug.Log ($"Banner OnDisable {placementId} {name}");
            StopAllCoroutines ();
            if (!Advertisement.isInitialized) return;
            Advertisement.Banner.Hide ();
        }
        IEnumerator LoadBanner ()
        {
            while (!Advertisement.IsReady (placementId))
                yield return new WaitForSeconds (0.5f);
            // Load won't show the banner, but just load it.
            Advertisement.Banner.Load (placementId);
        }
    }
}
