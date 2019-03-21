using System;

using UnityEngine;

namespace GameCore
{
    public partial class AdsController : ScriptableObject, IResetable
    {
        public void ResetDefault ()
        {
#if UNITY_EDITOR
            ValidateNoAds ();
#endif
        }
#if UNITY_EDITOR
        [ContextMenu ("Validate NoAds Variable")]
        private void ValidateNoAds ()
        {
            const string NoAdsVariablePath = "Assets/Data/Variables/AdsController/NoAds.asset";
            noAds = Tools.ValidateSO<BoolVariable> (NoAdsVariablePath);
            noAds.SetSavable (true);
            noAds.Save ();
            this.Save ();
        }
#endif
        public bool IsLoaded_REWARDED_VIDEO => CurrentAdsNetworks.IsLoaded_REWARDED_VIDEO;
        public bool IsLoaded_INTERSTITIAL => CurrentAdsNetworks.IsLoaded_INTERSTITIAL;
        public void ActivateNoAds () { NoAds = true; }
        public bool internetNotReachable
        {
            get
            {
#if UNITY_EDITOR
                return false;
#else
                return Application.internetReachability == NetworkReachability.NotReachable;
#endif
            }
        }

        [SerializeField] AdsNetwork currentAdsNetworks = null;
        [SerializeField] AdsNetwork dummyAdsNetwork = null;
        public AdsNetwork CurrentAdsNetworks
        {
            get
            {
                AdsNetwork result = null;
#if UNITY_EDITOR
                result = dummyAdsNetwork;
#else
                if (currentAdsNetworks == null)
                    result = dummyAdsNetwork;
                else
                    result = currentAdsNetworks;
#endif
                return result;
            }
        }

        public void InitWithConsent (bool consentValue)
        {
            CurrentAdsNetworks.Init (consentValue);
        }

        [SerializeField] BoolVariable noAds;
        [SerializeField] bool _debug;
        private string _currentPlacement;
        public string CurrentPlacement { set => _currentPlacement = value; }

        private bool NoAds { get { return noAds.Value; } set { noAds.Value = value; noAds.SaveValue (); } }
        event Action LastAds;
        public void ShowLastAdsShowed ()
        {
            CurrentAdsNetworks.CacheLastTryShowedAds ();
            LastAds?.Invoke ();
        }
        public void Show_REWARDED_VIDEO () => Show_REWARDED_VIDEO ("REWARDED_VIDEO");
        public void Show_REWARDED_VIDEO (string placement = "REWARDED_VIDEO")
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.ShowRewardVideo({placement.black()})\n {CurrentAdsNetworks}", this);

            LastAds = () => CurrentAdsNetworks.Show_REWARDED_VIDEO (placement);
            LastAds.Invoke ();
        }
        public void Show_INTERSTITIAL () => Show_INTERSTITIAL ("INTERSTITIAL");

        public void Show_INTERSTITIAL (string placesment = "INTERSTITIAL")
        {
            if (_debug)
                Debug.Log ($"{this.Log()}.Show INTERSTITIAL({placesment.black()}", this);

            LastAds = () => CurrentAdsNetworks.Show_INTERSTITIAL (placesment);
            LastAds.Invoke ();
        }
        public void Cache_INTERSTITIAL () => CurrentAdsNetworks.Load (AdType.INTERSTITIAL);
        public void Cache_REWARDED_VIDEO () => CurrentAdsNetworks.Load (AdType.REWARDED_VIDEO);
        public void CacheLastTryShowedAds ()
        {
            CurrentAdsNetworks.CacheLastTryShowedAds ();
        }

        public void Show_BANNER_BOTTOM (string placement)
        {
            LastAds = () => CurrentAdsNetworks.Show_BANNER_BOTTOM (placement);
            LastAds.Invoke ();
        }
        public void Hide_BANNER_BOTTOM ()
        {
            CurrentAdsNetworks.Hide_BANNER_BOTTOM ();
        }
    }
}
