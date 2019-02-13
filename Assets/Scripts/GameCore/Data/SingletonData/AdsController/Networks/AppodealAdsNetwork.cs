using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using System;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/AdsController/AdsNetwork/Appodeal")]
    public class AppodealAdsNetwork : AdsNetwork, IRewardedVideoAdListener, IInterstitialAdListener
    {
        public override bool isLoadedRewardVideo { get { return Appodeal.isLoaded (Appodeal.REWARDED_VIDEO); } }
        public override bool isLoadedInterstitial { get { return Appodeal.isLoaded (Appodeal.INTERSTITIAL); } }

        [Multiline]
        [SerializeField]
        string appKey;
        [SerializeField]
        AdType[] adTypes;
        AdType adType;
        [SerializeField]
        Network[] NetworksFromDisable;
        Network[] EnableOnly;
        [SerializeField] bool disableWriteExternalStoragePermissionCheck;
        private Action OnIntrastitialShown;
        // private Action onRewardedVideoLoaded;
        public override Action<bool> OnRewardedVideoLoaded { get { return onRewardedVideoLoaded; } }
        private Action<double, string> _onVideShown;

        public override void Init (bool consentValue)
        {
            foreach (var type in adTypes)
                adType |= type;

            Appodeal.setRewardedVideoCallbacks (this);
            Appodeal.setInterstitialCallbacks (this);

            if (disableWriteExternalStoragePermissionCheck)
                Appodeal.disableWriteExternalStoragePermissionCheck ();

            foreach (var network in NetworksFromDisable)
                Appodeal.disableNetwork (network.ToString ());
            Appodeal.initialize (appKey, (int) adType, consentValue);
        }
        public override void ShowIntrastitial (string playsment = "default", Action _onIntrastitialShown = null)
        {
            OnIntrastitialShown = _onIntrastitialShown;
            Appodeal.show (Appodeal.INTERSTITIAL);
        }
        public override void ShowRewardVideo (Action<double, string> OnVideoShown = null)
        {
            _onVideShown = OnVideoShown;
            Appodeal.show (Appodeal.REWARDED_VIDEO);
        }
        public void onInterstitialLoaded (bool isPrecache) { }
        public void onInterstitialFailedToLoad () { }
        public void onInterstitialShown () { if (OnIntrastitialShown != null) { OnIntrastitialShown (); } }
        public void onInterstitialClosed () { }
        public void onInterstitialClicked () { }
        public void onInterstitialExpired () { }
        public void onRewardedVideoLoaded (bool precache)
        {
            // Appodeal.
            // UserSettings userSettings = 
        }
        public void onRewardedVideoFailedToLoad () { }
        public void onRewardedVideoShown () { }
        public void onRewardedVideoFinished (double amount, string name)
        {
            if (_onVideShown != null) _onVideShown (amount, name);
        }
        public void onRewardedVideoClosed (bool finished) { }
        public void onRewardedVideoExpired () { }
    }

    [System.Flags]
    public enum AdType
    {
        INTERSTITIAL = Appodeal.INTERSTITIAL,
        REWARDED_VIDEO = Appodeal.REWARDED_VIDEO,
        BANNER = Appodeal.BANNER,
        BANNER_BOTTOM = Appodeal.BANNER_BOTTOM,
        BANNER_HORIZONTAL_CENTER = Appodeal.BANNER_HORIZONTAL_CENTER,
        BANNER_HORIZONTAL_LEFT = Appodeal.BANNER_HORIZONTAL_LEFT,
        BANNER_HORIZONTAL_RIGHT = Appodeal.BANNER_HORIZONTAL_RIGHT,
        BANNER_HORIZONTAL_SMART = Appodeal.BANNER_HORIZONTAL_SMART,
        BANNER_TOP = Appodeal.BANNER_TOP,
        BANNER_VIEW = Appodeal.BANNER_VIEW,
        MREC = Appodeal.MREC,
    }
    public enum Network
    {
        adcolony,
        admob,
        amazon_ads,
        applovin,
        appnext,
        avocarrot,
        chartboost,
        facebook,
        flurry,
        inmobi,
        inner_active,
        ironsource,
        mobvista,
        mailru,
        mmedia,
        mopub,
        ogury,
        openx,
        pubnative,
        smaato,
        startapp,
        tapjoy,
        unity_ads,
        vungle,
        yandex
    }
}
