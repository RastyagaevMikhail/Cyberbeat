using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CyberBeat
{
    [RequireComponent (typeof (GameEventListernerBool))]
    public class ButtonActionByVideoAds : MonoBehaviour
    {

        [HideInInspector][SerializeField] AdsController adsController;
        [SerializeField] GameObject Content;
        [SerializeField] GameObject NotInternet;
        [SerializeField] GameObject Waiting;
        [SerializeField] UnityEvent videoPreShow;
        [SerializeField] UnityEvent videoShowed;
        [SerializeField] UnityEvent videoUnvailable;
        private bool isLoaded => adsController.IsLoadedRewardVideo;
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
        bool isVideoUnvailable => internetNotReachable || !isLoaded;
        private void OnValidate ()
        {
            if (adsController == null) adsController = Resources.Load<AdsController> ("Data/AdsController");
        }
        private void Start ()
        {
            UpdateSatate ();
        }
        public void OnRewardedVideoLoaded (bool precache)
        {
            UpdateSatate ();
        }

        public void TryShowRewardVideo (string placement)
        {
            if (isVideoUnvailable)
                videoUnvailable.Invoke ();
            else
            {
                videoPreShow.Invoke ();
                adsController.ShowRewardVideo (placement, videoShowed.Invoke);
            }
        }
        public void TryShowRewardVideo ()
        {
            TryShowRewardVideo ("RewardVideo");
        }
        private void UpdateSatate ()
        {
            // Debug.LogFormat ("UpdateSatate = {0}.{1}", name, transform.parent.parent.name);
            if (Content)
                Content.SetActive (isLoaded);
            NotInternet.SetActive (!isLoaded && internetNotReachable);
            Waiting.SetActive (!isLoaded && !internetNotReachable);
        }
    }
}
