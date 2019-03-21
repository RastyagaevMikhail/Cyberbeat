using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using Timers;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CyberBeat
{
    public class AdsNotLoadedPopup : MonoBehaviour
    {
        [SerializeField] AdsController ads;
        [SerializeField] UnityEvent show;
        [SerializeField] UnityEvent hide;
        AdType currrentAdtype = AdType.INTERSTITIAL;
        [SerializeField] bool debug;

        public AdType CurrrentAdtype { set => currrentAdtype = value; }
        public int CurrrentAdtypeIntValue { set => currrentAdtype = (AdType) value; }
        public IntVariable CurrrentAdtypeIntVariable { set => CurrrentAdtypeIntValue = value.Value; }

        Dictionary<AdType, Action> cacheSelector => new Dictionary<AdType, Action> ()
        { { AdType.INTERSTITIAL, ads.Cache_INTERSTITIAL }, { AdType.REWARDED_VIDEO, ads.Cache_REWARDED_VIDEO }
        };
        Dictionary<AdType, bool> loadedSelector => new Dictionary<AdType, bool> ()
        { { AdType.INTERSTITIAL, ads.IsLoaded_INTERSTITIAL}, { AdType.REWARDED_VIDEO, ads.IsLoaded_REWARDED_VIDEO}
        };
        public void OnFailLoadOnShowWithPlacement (AdType adType, string placement)
        {
            currrentAdtype = adType;
            Show ();
        }
        public void ShowAds ()
        {
            ads.ShowLastAdsShowed ();
        }
        public void Show ()
        {
            show.Invoke ();
        }
        public void Hide ()
        {
            hide.Invoke ();
        }

        public void CacheCurrenAdType ()
        {
            TimersManager.SetTimer (this, 15, CheckCache); //? auto clear on set timer
            Action cahce = null;
            cacheSelector.TryGetValue (currrentAdtype, out cahce);
            if (cahce == null)
            {
                if (debug) Debug.Log ($"try cache AdType : {currrentAdtype} : {(int)currrentAdtype}");
                currrentAdtype = AdType.INTERSTITIAL;
                ads.Cache_INTERSTITIAL ();
            }
            else
                cahce ();
        }

        public void CheckCache ()
        {
            bool IsLoaded = false;
            loadedSelector.TryGetValue (currrentAdtype, out IsLoaded);
            if (!IsLoaded)
            {
                CacheCurrenAdType ();
            }

        }
    }
}
