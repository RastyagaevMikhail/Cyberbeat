using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/AdsController/AdsNetwork/Appodeal")]
    public class AppodealAdsNetwork : AdsNetwork, IRewardedVideoAdListener, IInterstitialAdListener, IBannerAdListener
    {
        [SerializeField] BoolVariable isLoaded_REWARDED_VIDEO;
        [SerializeField] BoolVariable isLoaded_INTERSTITIAL;
        public override bool IsLoaded_REWARDED_VIDEO => Appodeal.isLoaded (Appodeal.REWARDED_VIDEO);
        public override bool IsLoaded_INTERSTITIAL => Appodeal.isLoaded (Appodeal.INTERSTITIAL);
        [Multiline]
        [SerializeField] string appKey;
        [Header ("AdType Settings")]
        [SerializeField] AdType[] adTypes;
        [SerializeField] AdType[] adTypesFromCache;
        AdType adType;
        [SerializeField] CacheSettings[] cacheSettings;

        [SerializeField]
        [Header ("Networks Settings")]
        Network[] NetworksFromDisable;
        [Header ("Other Settings")]
        [SerializeField] bool disableWriteExternalStoragePermissionCheck;

        [Header ("Events")]
        [Header ("Interstitial")]
        [SerializeField] UnityEventString OnInterstitialFailedToLoad;
        [SerializeField] UnityEventBool OnIntrastitialLoaded; // precache
        [SerializeField] UnityEventString OnInterstitialShown; // placement
        [SerializeField] UnityEventString OnInterstitialClosed; // placement
        [SerializeField] UnityEventString OnInterstitialClicked; // placement
        [SerializeField] UnityEventString OnInterstitialExpired; // placement
        [Header ("Video Reward")]
        [SerializeField] UnityEventBool OnRewardedVideoLoaded; // precache
        [SerializeField] UnityEventRewardVideo OnRewardVideoShown; //amount, name
        [SerializeField] UnityEventString OnRewardedVideoFailedToLoad; // placement
        [SerializeField] UnityEventString OnRewardedVideoShown; // placement
        [Tooltip ("finished")]
        [SerializeField] UnityEventBool OnRewardedVideoClosed; // finished
        [SerializeField] UnityEventString OnRewardedVideoExpired; // placement
        [Header ("Banner")]
        [SerializeField] UnityEventAdTypeString OnNotLoadeAdsWithPlacement;
        [SerializeField] bool debug;
        string currentPlacement;
        int currentAdType;
        [ContextMenu ("TestAdTypesValues")]
        void TestAdTypesValues ()
        {
            foreach (var type in adTypes)
                adType |= type;

            if (debug) Debug.Log ($"adType = {adType} : {(int)adType}");

            int adTypeIntValue = 0;
            foreach (var type in adTypes)
                adTypeIntValue |= (int) type;

            if (debug) Debug.Log ($"adType = {adType} : {adTypeIntValue}");
        }
        public override void Init (bool consentValue)
        {
            foreach (var type in adTypes)
                adType |= type;

            if (debug) Debug.Log ($"adType = {adType} : {(int)adType}");

            if (disableWriteExternalStoragePermissionCheck)
                Appodeal.disableWriteExternalStoragePermissionCheck ();

            foreach (var network in NetworksFromDisable)
                Appodeal.disableNetwork (network.ToString ());

            foreach (var setting in cacheSettings)
                setting.Initialize (debug);

            Appodeal.setRewardedVideoCallbacks (this);
            Appodeal.setInterstitialCallbacks (this);
            Appodeal.setBannerCallbacks (this);

            //!!!--------------------INITIALIZE-------------------!!!\\

            Appodeal.initialize (appKey, (int) adType, consentValue);
            foreach (AdType adType in adTypesFromCache)
            {
                int adTypeIntValue = (int) adType;
                bool isPrecache = Appodeal.isPrecache (adTypeIntValue);
                Appodeal.setTriggerOnLoadedOnPrecache (adTypeIntValue, isPrecache);
                if (debug) Debug.Log ($"Cache {adType} : {adTypeIntValue} \nisPrecache = {isPrecache}");
                Cache (adType);
            }

        }

        [ContextMenu ("ShowDisabledNetworks")]
        void ShowDisabledNetworks ()
        {
            foreach (var network in NetworksFromDisable)
                Debug.Log (network.ToString ());
        }

        public override void Show_INTERSTITIAL (string placement = "INTERSTITIAL")
        {
            if (debug) Debug.Log ($"AppodealAdsNetwork.ShowIntrastitial(placement:\"{placement}\") \nIsLoaded_INTERSTITIAL = {IsLoaded_INTERSTITIAL}");

            currentPlacement = placement;
            currentAdType = Appodeal.INTERSTITIAL;

            Debug.Log ($"Can Show INTERSTITIAL placement: {placement} \n{Appodeal.canShow(Appodeal.INTERSTITIAL,placement)}");

            if (this.IsLoaded_INTERSTITIAL)
            {
                bool isSheldue = Appodeal.show (Appodeal.INTERSTITIAL, placement);

                if (debug) Debug.Log ($"isSheldue:{isSheldue}");

            }
            else
            {
                OnNotLoadeAdsWithPlacement.Invoke (AdType.INTERSTITIAL, placement);
                // bool isPrecache = Appodeal.isPrecache (Appodeal.INTERSTITIAL);
                // Appodeal.setTriggerOnLoadedOnPrecache (Appodeal.INTERSTITIAL, isPrecache);

                // if (debug) Debug.Log ($"isPrecache INTERSTITIAL = {isPrecache}");
                // Cache (AdType.INTERSTITIAL);
            }
        }

        public override void CacheLastTryShowedAds ()
        {
            if (debug) Debug.Log ($"Cache {((AdType)currentAdType).ToString()}");
            Appodeal.cache (currentAdType);
        }

        public override void Show_REWARDED_VIDEO (string placement = "REWARDED_VIDEO")
        {

            if (debug) Debug.Log ($"Show_REWARDED_VIDEO(placement:\"{placement}\") \nIsLoaded_REWARDED_VIDEO = {IsLoaded_REWARDED_VIDEO}");

            currentPlacement = placement;
            currentAdType = Appodeal.REWARDED_VIDEO;

            Debug.Log ($"Can Show REWARDED_VIDEO  placement: {placement} \n{Appodeal.canShow(Appodeal.REWARDED_VIDEO, placement)}");

            if (IsLoaded_REWARDED_VIDEO)
            {
                bool isSheldue = Appodeal.show (Appodeal.REWARDED_VIDEO, placement);

                if (debug) Debug.Log ($"isSheldue:{isSheldue}");
            }
            else
            {
                OnNotLoadeAdsWithPlacement.Invoke (AdType.REWARDED_VIDEO, placement);
                bool isPrecache = Appodeal.isPrecache (Appodeal.REWARDED_VIDEO);
                Appodeal.setTriggerOnLoadedOnPrecache (Appodeal.REWARDED_VIDEO, isPrecache);

                if (debug) Debug.Log ($"isPrecache REWARDED_VIDEO = {isPrecache}");
                Cache (AdType.INTERSTITIAL);
            }
        }

        public override void Show_BANNER_BOTTOM (string placement = "BANNER")
        {
            Appodeal.show (Appodeal.BANNER_BOTTOM, placement);
        }
        public override void Hide_BANNER_BOTTOM ()
        {
            Appodeal.hide (Appodeal.BANNER_BOTTOM);
        }
        public override void Cache (AdType adType)
        {
            int adTypeIntValue = (int) adType;
            if (debug) Debug.Log ($"Cahce {adType} = {adTypeIntValue}");
            Appodeal.cache (adTypeIntValue);
        }

        #region Interstitial Callbacks 
        public void onInterstitialLoaded (bool isPrecache)
        {
            if (debug) Debug.Log ($"onInterstitialLoaded(isPrecache:{isPrecache})");
            isLoaded_INTERSTITIAL.Value = IsLoaded_INTERSTITIAL;
            OnIntrastitialLoaded.Invoke (isPrecache);
        }
        public void onInterstitialFailedToLoad ()
        {
            if (debug) Debug.Log ($"onInterstitialFailedToLoad(currentPlacement:{currentPlacement})");
            isLoaded_INTERSTITIAL.Value = IsLoaded_INTERSTITIAL;
            OnInterstitialFailedToLoad.Invoke (currentPlacement);
        }
        public void onInterstitialShown ()
        {
            if (debug) Debug.Log ($"onInterstitialShown currentPlacement:\"{currentPlacement}\" ");
            OnInterstitialShown.Invoke (currentPlacement);
        }
        public void onInterstitialClosed ()
        {
            if (debug) Debug.Log ($"onInterstitialClosed currentPlacement:\"{currentPlacement}\" ");
            OnInterstitialClosed.Invoke (currentPlacement);
        }
        public void onInterstitialClicked ()
        {
            if (debug) Debug.Log ($"onInterstitialClicked currentPlacement:\"{currentPlacement}\" ");
            OnInterstitialClicked.Invoke (currentPlacement);
        }
        public void onInterstitialExpired ()
        {
            if (debug) Debug.Log ($"onInterstitialExpired currentPlacement:\"{currentPlacement}\" ");
            OnInterstitialExpired.Invoke (currentPlacement);
        }
        #endregion

        #region RewardVideo Callbacks 
        public void onRewardedVideoLoaded (bool isPrecache)
        {
            if (debug) Debug.Log ($"onRewardedVideoLoaded(isPrecache:{isPrecache})");
            isLoaded_REWARDED_VIDEO.Value = IsLoaded_REWARDED_VIDEO;
            OnRewardedVideoLoaded.Invoke (isPrecache);
        }
        public void onRewardedVideoFailedToLoad ()
        {
            if (debug) Debug.Log ($"onRewardedVideoFailedToLoad(currentPlacement:{currentPlacement})");
            isLoaded_REWARDED_VIDEO.Value = IsLoaded_REWARDED_VIDEO;
            OnRewardedVideoFailedToLoad.Invoke (currentPlacement);
        }
        public void onRewardedVideoShown ()
        {
            if (debug) Debug.Log ($"onRewardedVideoShown currentPlacement:\"{currentPlacement}\" ");
            OnRewardedVideoShown.Invoke (currentPlacement);
        }
        public void onRewardedVideoFinished (double amount, string name)
        {
            if (debug) Debug.Log ($"onRewardedVideoFinished currentPlacement:\"{currentPlacement}\" ");
            OnRewardVideoShown.Invoke (amount, name);
        }
        public void onRewardedVideoClosed (bool finished)
        {
            if (debug) Debug.Log ($"onRewardedVideoClosed currentPlacement:\"{currentPlacement}\" ");
            OnRewardedVideoClosed.Invoke (finished);
        }
        public void onRewardedVideoExpired ()
        {
            if (debug) Debug.Log ($"onRewardedVideoExpired currentPlacement:\"{currentPlacement}\" ");
            OnRewardedVideoExpired.Invoke (currentPlacement);
        }

        #endregion
        #region Banner Callbacks
        public void onBannerLoaded (bool isPrecache) { }
        public void onBannerFailedToLoad () { }
        public void onBannerShown () { }
        public void onBannerClicked () { }
        public void onBannerExpired () { }

        #endregion
#if UNITY_EDITOR
        [ContextMenu ("GenerateAdTypeVariables")]
        void GenerateAdTypeVariables ()
        {
            foreach (AdType adType in System.Enum.GetValues (typeof (AdType)))
            {
                var AdTypeVariable =
                    Tools.ValidateSO<IntVariable> ($"Assets/Data/Variables/AdsController/AdType/{adType}.asset");
                AdTypeVariable.Value = (int) adType;
            }
        }
#endif
    }
}
