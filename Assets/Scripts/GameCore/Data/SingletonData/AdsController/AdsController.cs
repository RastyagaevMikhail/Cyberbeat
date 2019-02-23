﻿using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public partial class AdsController : SingletonData<AdsController>, IStartInitializationData
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/ADS")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void ResetDefault () { }
        public override void InitOnCreate ()
        {
            CreateNoAdsVariable ();
        }

        [ContextMenu ("Create NoAds Variable")]
        private void CreateNoAdsVariable ()
        {
            const string NoAdsVariablePath = "Assets/Data/Variables/AdsController/NoAds.asset";
            noAds = Tools.ValidateSO<BoolVariable> (NoAdsVariablePath);
            noAds.isSavable = true;
            this.Save ();

        }
#else
        public override void ResetDefault () { }
#endif

        public bool isLoadedRewardVideo { get { return CurrentAdsNetworks.isLoadedRewardVideo; } }
        public bool isLoadedInterstitial { get { return CurrentAdsNetworks.isLoadedInterstitial; } }

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

        public void Init (bool consentValue)
        {
            CurrentAdsNetworks.Init (consentValue);
            OnRewardedVideoLoaded += CurrentAdsNetworks.OnRewardedVideoLoaded;
        }

        Action<double, string> _onVideShown;
        [NonSerialized] public Action<bool> OnRewardedVideoLoaded;
        [SerializeField] BoolVariable noAds;
        [SerializeField] bool _debug;

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
        public void ShowRewardVideo (Action OnVideoShown)
        {
            ShowRewardVideo ((a, n) => OnVideoShown ());

        }
        public void ShowRewardVideo (Action<double, string> OnVideoShown = null)
        {
            if (_debug)
            {
                Debug.LogFormat ("AdsController.ShowRewardVideo(OnVideoShown = {0})", OnVideoShown);
                Debug.LogFormat ("AdsController.ShowRewardVideo.CurrentAdsNetworks = {0}", CurrentAdsNetworks);
            }
            CurrentAdsNetworks.ShowRewardVideo (OnVideoShown);
        }

        public void ShowIntrastitial ()
        {
            ShowIntrastitial ("default", null);
        }

        public void ShowIntrastitial_GE (GameEvent gameEvent)
        {
            ShowIntrastitial ("default", gameEvent.Raise);
        }
        public void ShowIntrastitial (string playsment = "default")
        {
            ShowIntrastitial (playsment, null);
        }
        public void ShowIntrastitial (string playsment = "default", Action _onIntrastitialShown = null)
        {
            if (_debug)
            {
                Debug.LogFormat ("AdsController.ShowIntrastitial(playsment = {0}, _onIntrastitialShown = {1})", playsment, _onIntrastitialShown);
                Debug.LogFormat ("AdsController.ShowIntrastitial.CurrentAdsNetworks = {0}", CurrentAdsNetworks);
            }

            CurrentAdsNetworks.ShowIntrastitial (playsment, _onIntrastitialShown);
        }
    }

    [Serializable] public class RewardVideoUnityEvent : UnityEvent<double, string> { }

}
