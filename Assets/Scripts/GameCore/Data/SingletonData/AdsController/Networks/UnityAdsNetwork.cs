using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Monetization;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/AdsController/AdsNetwork/UnityAds")]
    public class UnityAdsNetwork : AdsNetwork
    {

        [SerializeField] string gameID;
        [SerializeField] bool TestMode = false;

        public override bool IsLoaded_REWARDED_VIDEO =>
            current_REWARDED_VIDEO != null?current_REWARDED_VIDEO.ready : false;

        public override bool IsLoaded_INTERSTITIAL =>
            current_INTERSTITIAL != null?current_INTERSTITIAL.ready : false;

        public override void Load (AdType INTERSTITIAL) { }

        public override void CacheLastTryShowedAds () { }

        public override void Hide_BANNER_BOTTOM () { }

        public override void Init (bool consestValue)
        {
            Debug.Log ($"Initialize UnityAds with GameID {gameID}");
            Monetization.Initialize (gameID, TestMode);
        }

        public override void Show_BANNER_BOTTOM (string placement = "BANNER")
        {

        }
        [SerializeField] ShowResultUnityEventStringSelector INTERSTITIAL_Callbacks;
        [SerializeField] ShowResultUnityEventStringSelector REWARDED_VIDEO_Callbacks;
        ShowAdPlacementContent current_INTERSTITIAL;
        ShowAdPlacementContent current_REWARDED_VIDEO;
        public override void Show_INTERSTITIAL (string placement = "INTERSTITIAL")
        {
            UniyAdsObject.ShowAd (placement, INTERSTITIAL_Callbacks);
        }
        public override void Show_REWARDED_VIDEO (string placement = "REWARDED_VIDEO")
        {
            UniyAdsObject.ShowAd (placement, REWARDED_VIDEO_Callbacks);
        }
    }
}
