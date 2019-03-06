using System;

using UnityEngine;
namespace GameCore
{
    public abstract class AdsNetwork : ScriptableObject
    {
        public abstract event Action<bool> OnRewardedVideoLoaded;

        public abstract void Init (bool consestValue);
		public abstract bool IsLoadedRewardVideo { get; }

		public abstract bool IsLoadedInterstitial { get; }

		public abstract void ShowRewardVideo (string palcement, Action<double, string> OnVideoShown = null);
        public abstract void ShowIntrastitial (string playsment = "default", Action _onIntrastitialShown = null);
        public abstract void Show_BANNER_BOTTOM (string placement = null);
        public abstract void Hide_BANNER_BOTTOM ();
    }
}
