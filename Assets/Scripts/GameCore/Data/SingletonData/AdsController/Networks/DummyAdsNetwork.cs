using System;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/AdsController/AdsNetwork/Dummy")]
    public class DummyAdsNetwork : AdsNetwork
    {
        [SerializeField] bool _debug = true;
        [SerializeField] bool _isLoadedRewardVideo = true;
        public override bool isLoadedRewardVideo
        {
            get
            {
                if (_debug)
                    Debug.LogFormat ("DummyAdsNetwork.isLoadedRewardVideo = {0}", _isLoadedRewardVideo);
                return _isLoadedRewardVideo;
            }
        }

        [SerializeField] bool _isLoadedInterstitial = true;
        public override bool isLoadedInterstitial
        {
            get
            {
                if (_debug)
                    Debug.LogFormat ("DummyAdsNetwork.isLoadedInterstitial = {0}", _isLoadedInterstitial);
                return _isLoadedInterstitial;
            }
        }
        public void onRewardVideoLoaded (bool precache)
        {
            if (_debug) Debug.LogFormat ("DummyAdsNetwork.OnRewardedVideoLoaded (precache = {0})", precache);
        }
        public override event Action<bool> OnRewardedVideoLoaded
        {
            add => value += onRewardVideoLoaded;
            remove => value -= onRewardVideoLoaded;
        }

        public override void Init (bool consestValue)
        {
            if (_debug)
                Debug.LogFormat ("DummyAdsNetwork.Init(consestValue = {0})", consestValue);
        }

        public override void ShowIntrastitial (string playsment = "default", Action _onIntrastitialShown = null)
        {
            if (_debug)
                Debug.LogFormat ("DummyAdsNetwork.ShowIntrastitial(playsment = {0}, _onIntrastitialShown = {1})", playsment, _onIntrastitialShown);
            if (_onIntrastitialShown != null) _onIntrastitialShown ();
        }

        public override void ShowRewardVideo (Action<double, string> OnVideoShown = null)
        {
            if (_debug)
                Debug.LogFormat ("DummyAdsNetwork.ShowRewardVideo(OnVideoShown = {0})", OnVideoShown);
            if (OnVideoShown != null) OnVideoShown (0, "");
        }
    }
}
