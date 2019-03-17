using System;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/AdsController/AdsNetwork/Dummy")]
    public class DummyAdsNetwork : AdsNetwork
    {
        [SerializeField] bool _debug = true;
        [SerializeField] bool _isLoadedRewardVideo = true;

        public override bool IsLoaded_REWARDED_VIDEO
        {
            get
            {
                if (_debug)
                    Debug.LogFormat ("DummyAdsNetwork.isLoadedRewardVideo = {0}", _isLoadedRewardVideo);
                return _isLoadedRewardVideo;
            }
        }

        [SerializeField] bool _isLoadedInterstitial = true;
        [SerializeField] UnityEventString interstitialShown;

        public override bool IsLoaded_INTERSTITIAL
        {
            get
            {
                if (_debug)
                    Debug.LogFormat ("DummyAdsNetwork.isLoadedInterstitial = {0}", _isLoadedInterstitial);
                return _isLoadedInterstitial;
            }
        }

        [ContextMenu ("onRewardVideoLoaded")]
        void onRewardVideoLoaded ()
        {
            onRewardVideoLoaded (false);
        }
        public void onRewardVideoLoaded (bool precache)
        {
            _isLoadedRewardVideo = true;
            if (_debug) Debug.LogFormat ("DummyAdsNetwork.OnRewardedVideoLoaded (precache = {0})", precache);
        }

        public override void Init (bool consestValue)
        {
            if (_debug)
                Debug.LogFormat ("DummyAdsNetwork.Init(consestValue = {0})", consestValue);
        }

        public override void Show_INTERSTITIAL (string playsment = "default")
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.ShowIntrastitial( {playsment})", this);
            interstitialShown.Invoke (playsment);
        }

        [SerializeField] UnityEventRewardVideo onRewardVideoFinished;
        [SerializeField] UnityEventString onRewardVideoShown;
        public override void Show_REWARDED_VIDEO (string placement)
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.ShowRewardVideo({placement})", this);
            onRewardVideoFinished.Invoke (0, "");
            onRewardVideoShown.Invoke (placement);
        }

        public override void Show_BANNER_BOTTOM (string placement = null)
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.Show_BANNER_BOTTOM({placement})", this);
        }

        public override void Hide_BANNER_BOTTOM ()
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.Hide_BANNER_BOOTOM()", this);
        }

        public override void CacheLastTryShowedAds ()
        {
            if (_debug) Debug.Log ("Cache Intrasititial");
        }

        public override void Cache (AdType adType)
        {
            if (_debug) Debug.Log ($"Cache {adType}");
        }
    }
}
