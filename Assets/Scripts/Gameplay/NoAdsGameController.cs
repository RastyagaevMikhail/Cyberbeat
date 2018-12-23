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

        [SerializeField] GameEvent ShowIntrastiotialAds;
        [SerializeField] GameEvent RestartGame;

        [SerializeField] TimeSpanVariable NoAdsTime;
        [SerializeField] BoolVariable NoAdsIsEnabled;

        bool noAdsIsEnabled { get { return NoAdsIsEnabled.Value; } set { NoAdsIsEnabled.Value = value; } }

        private void Awake ()
        {
            if (noAdsIsEnabled) NoAdsTimer.StartTimer ();
        }

        public void _OnChngeNoAdsState (bool isEnabled)
        {
            if (isEnabled)
            {
                Ads.ShowIntrastitial ("Autogenration", OnAdsShown);
            }
        }

        private void OnAdsShown ()
        {

        }

        public void OnPlayerDeath ()
        {
            if (!noAdsIsEnabled)
                ShowIntrastiotialAds.Raise ();
            else
                RestartGame.Raise ();
        }
    }
}
