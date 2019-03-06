using System;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/AdsController/AdsNetwork/Dummy")]
    public class DummyAdsNetwork : AdsNetwork
    {
        [SerializeField] bool _debug = true;
        [SerializeField] bool _isLoadedRewardVideo = true;

        public override bool IsLoadedRewardVideo
        {
            get
            {
                if (_debug)
                    Debug.LogFormat ("DummyAdsNetwork.isLoadedRewardVideo = {0}", _isLoadedRewardVideo);
                return _isLoadedRewardVideo;
            }
        }

        [SerializeField] bool _isLoadedInterstitial = true;

        public override bool IsLoadedInterstitial
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
                Debug.Log ($"{this.Log()}.ShowIntrastitial( {playsment}, {_onIntrastitialShown})", this);
            if (_onIntrastitialShown != null) _onIntrastitialShown ();
        }

        public override void ShowRewardVideo (string placement, Action<double, string> OnVideoShown = null)
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.ShowRewardVideo({placement},{OnVideoShown})", this);
            if (OnVideoShown != null) OnVideoShown (0, "");
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
    }
}
