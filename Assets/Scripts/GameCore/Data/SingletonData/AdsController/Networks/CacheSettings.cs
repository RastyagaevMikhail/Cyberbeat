using System;
using AppodealAds.Unity.Api;
using UnityEngine;

namespace GameCore
{
    [Serializable]
    public class CacheSettings
    {
        [SerializeField] AdType adType = AdType.REWARDED_VIDEO;
        [SerializeField] bool autoCache = true;

        public void Initialize (bool debug = false)
        {
            if (debug) Debug.Log ($"Initialize {adType}: autoCache = {autoCache}");
            Appodeal.setAutoCache ((int) adType, autoCache);
        }
    }
}
