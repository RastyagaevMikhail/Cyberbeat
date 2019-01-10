using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class NoAdsGameController : MonoBehaviour
    {
        [SerializeField] TimeSpanTimerAction NoAdsTimer;
        public AdsController Ads { get { return AdsController.instance; } }

        [SerializeField] GameEvent ShowIntrstitialAdsTimer;
        [SerializeField] GameEvent RestartGame;

        [SerializeField] BoolVariable NoAdsIsEnabled;

        bool noAdsIsEnabled { get { return NoAdsIsEnabled.Value; } set { NoAdsIsEnabled.Value = value; } }

        private void Awake ()
        {
            if (noAdsIsEnabled) NoAdsTimer.StartTimer ();
        }
        int notLoadedAds;
        public void OnCheckNoAds ()
        {
            if (noAdsIsEnabled)
            {
                RestartGame.Raise ();//need Delay
            }
            else
            {
                ShowIntrstitialAdsTimer.Raise ();
                Ads.ShowIntrastitial ();
            }
        }
    }
}
