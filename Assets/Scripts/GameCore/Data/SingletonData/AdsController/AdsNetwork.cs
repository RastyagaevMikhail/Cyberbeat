using System;

using UnityEngine;
namespace GameCore
{
    public abstract class AdsNetwork : ScriptableObject
    {
        public abstract void Init (bool consestValue);
        public abstract bool IsLoaded_REWARDED_VIDEO { get; }
        public abstract bool IsLoaded_INTERSTITIAL { get; }

        public abstract void Show_REWARDED_VIDEO (string placement = "REWARDED_VIDEO");
        public abstract void Show_INTERSTITIAL (string placement = "INTERSTITIAL");
        public abstract void Show_BANNER_BOTTOM (string placement = "BANNER");
        public abstract void Hide_BANNER_BOTTOM ();
        public abstract void CacheLastTryShowedAds ();
        public abstract void Load (AdType INTERSTITIAL);
    }
}
