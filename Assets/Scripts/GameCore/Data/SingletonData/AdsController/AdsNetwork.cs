using System;

using UnityEngine;
namespace GameCore
{
    public abstract class AdsNetwork : ScriptableObject
    {
        public abstract event Action<bool> OnRewardedVideoLoaded;

        public abstract void Init (bool consestValue);
        public abstract bool isLoadedRewardVideo { get; }
        public abstract bool isLoadedInterstitial { get; }

        public abstract void ShowRewardVideo (string palcement, Action<double, string> OnVideoShown = null);
        public abstract void ShowIntrastitial (string playsment = "default", Action _onIntrastitialShown = null);
    }
}
