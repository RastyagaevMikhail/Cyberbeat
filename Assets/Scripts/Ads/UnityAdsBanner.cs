using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Advertisements;

namespace GameCore
{
    public class UnityAdsBanner : MonoBehaviour
    {
        [SerializeField] string placementId = "BANNER";
        private void OnEnable ()
        {
            Debug.Log ($"Banner OnEnabel {placementId} {name}");
            ShowAd ();
        }

        [SerializeField] StringVariable gameID;
        [SerializeField] BoolVariable noAds;
        bool NoAds => noAds.Value;
#if UNITY_EDITOR
        private void OnValidate ()
        {
            gameID = Tools.ValidateSO<StringVariable> ("Assets/Resources/Data/Variables/UnitAds/GameID.asset");
            noAds = Tools.ValidateSO<BoolVariable> ("Assets/Data/Variables/AdsController/NoAds.asset");
        }
#endif

        private void InititalizeIfNeed ()
        {
            if (NoAds) return;
            if (!Advertisement.isInitialized)
                Advertisement.Initialize (gameID.Value, false);
        }
        public void ShowAd ()
        {
            Debug.Log ($"Banner onShow NoAds {NoAds} {placementId} {name}");
            if (NoAds) return;
            StartCoroutine (ShowBannerWhenReady (placementId));
        }
        IEnumerator ShowBannerWhenReady (string placementId)
        {
            while (!Advertisement.IsReady (placementId))
                yield return new WaitForSeconds (0.5f);
            Debug.Log ($"Banner Ready {placementId} {name}");
            Advertisement.Banner.Show (placementId);
        }
        private void OnDisable ()
        {
            Debug.Log ($"Banner onHide NoAds {NoAds} {placementId} {name}");
            if (NoAds) return;
            Debug.Log ($"Banner OnDisable {placementId} {name}");
            StopAllCoroutines ();
            Advertisement.Banner.Hide ();
        }
    }
}
