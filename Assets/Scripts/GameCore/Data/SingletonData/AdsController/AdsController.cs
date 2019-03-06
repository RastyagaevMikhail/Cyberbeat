using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public partial class AdsController : ScriptableObject, IResetable
    {
        public void ResetDefault () { ValidateNoAds (); }

        [ContextMenu ("Validate NoAds Variable")]
        private void ValidateNoAds ()
        {
            const string NoAdsVariablePath = "Assets/Data/Variables/AdsController/NoAds.asset";
            noAds = Tools.ValidateSO<BoolVariable> (NoAdsVariablePath);
            noAds.SetSavable (true);
            noAds.Save ();
            this.Save ();
        }

        public bool IsLoadedRewardVideo { get { return CurrentAdsNetworks.IsLoadedRewardVideo; } }
        public bool isLoadedInterstitial { get { return CurrentAdsNetworks.IsLoadedInterstitial; } }

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
            CurrentAdsNetworks.OnRewardedVideoLoaded += onRewardedVideoLoaded.Invoke;
        }
        private void OnDisable ()
        {
            CurrentAdsNetworks.OnRewardedVideoLoaded -= onRewardedVideoLoaded.Invoke;
        }

        Action<double, string> _onVideShown;
        [SerializeField] UnityEventBool onRewardedVideoLoaded;
        [SerializeField] BoolVariable noAds;
        [SerializeField] bool _debug;
        public string CurrentPlacement { get; set; }

        private bool NoAds { get { return noAds.Value; } set { noAds.Value = value; noAds.SaveValue (); } }

        public void ShowRewardVideo (GameEventRewardVideo OnVideoShown)
        {
            ShowRewardVideo (OnVideoShown.Raise);
        }
        public void ShowRewardVideo (RewardVideoUnityEvent OnVideoShown)
        {
            ShowRewardVideo (OnVideoShown.Invoke);
        }
        public void ShowRewardVideo (GameEvent OnVideoShown)
        {
            ShowRewardVideo (OnVideoShown.Raise);
        }
        public void ShowRewardVideo ()
        {
            ShowRewardVideo ((a, n) => { });
        }
        public void ShowRewardVideo (string placemnt, Action OnVideoShown)
        {
            CurrentPlacement = placemnt;
            ShowRewardVideo ((a, n) => OnVideoShown ());
        }
        public void ShowRewardVideo (string placemnt, Action<double, string> OnVideoShown = null)
        {
            CurrentPlacement = placemnt;
            ShowRewardVideo (OnVideoShown);
        }
        public void ShowRewardVideo (string placemnt)
        {
            CurrentPlacement = placemnt;
            ShowRewardVideo ((a, n) => { });
        }
        public void ShowRewardVideo (Action OnVideoShown)
        {
            ShowRewardVideo ((a, n) => OnVideoShown ());
        }
        public void ShowRewardVideo (Action<double, string> OnVideoShown = null)
        {
            if (_debug)
            {
                Debug.Log ($"{this.Log()}.ShowRewardVideo({CurrentPlacement.black()},{OnVideoShown.Log()})\n {CurrentAdsNetworks}", this);
            }
            CurrentAdsNetworks.ShowRewardVideo (CurrentPlacement, OnVideoShown);
        }

        public void ShowIntrastitial ()
        {
            ShowIntrastitial ("default", null);
        }

        public void ShowIntrastitial_GE (GameEvent gameEvent)
        {
            ShowIntrastitial ("Inrsatitial", gameEvent.Raise);
        }
        public void ShowIntrastitial (string playsment = "Inrsatitial")
        {
            ShowIntrastitial (playsment, null);
        }
        public void ShowIntrastitial (string playsment = "Inrsatitial", Action _onIntrastitialShown = null)
        {
            if (_debug)
            {
                Debug.Log ($"{this.Log()}.ShowIntrastitial({playsment.black()}, {_onIntrastitialShown.Log()})", this);
            }

            CurrentAdsNetworks.ShowIntrastitial (playsment, _onIntrastitialShown);
        }

        public void Show_BANNER_BOTTOM (string placement)
        {
            CurrentAdsNetworks.Show_BANNER_BOTTOM (placement);
        }
        public void Hide_BANNER_BOTTOM ()
        {
            CurrentAdsNetworks.Hide_BANNER_BOTTOM ();
        }
    }

    [Serializable] public class RewardVideoUnityEvent : UnityEvent<double, string> { }

}
