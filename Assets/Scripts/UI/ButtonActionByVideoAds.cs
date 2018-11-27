using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class ButtonActionByVideoAds : MonoBehaviour, IRewardedVideoAdListener
    {

        [SerializeField] GameObject text;
        [SerializeField] GameObject NotInternet;
        [SerializeField] GameObject Waiting;
        private bool isLoaded
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                return Appodeal.isLoaded (Appodeal.REWARDED_VIDEO);
#endif
            }
        }
        bool internetNotReachable
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
        private void Start ()
        {
            Appodeal.setRewardedVideoCallbacks (this);
            text.SetActive (isLoaded);
            NotInternet.SetActive (!isLoaded && internetNotReachable);
            Waiting.SetActive (!isLoaded && !internetNotReachable);
            Waiting.transform.DORotate (Vector3.forward * 90, 1f, RotateMode.LocalAxisAdd).SetLoops (-1, LoopType.Incremental);
        }
        Action _onVideShown;
        public void Init (Action OnVideoShown)
        {
            _onVideShown = OnVideoShown;
        }
        public void ShowVideo ()
        {
#if UNITY_EDITOR
            onRewardedVideoShown ();
#else
            Appodeal.show (Appodeal.REWARDED_VIDEO);
#endif
        }
        public void onRewardedVideoClosed (bool finished)
        {

        }

        public void onRewardedVideoExpired () { }

        public void onRewardedVideoFailedToLoad ()
        {
            NotInternet.SetActive (internetNotReachable);
            Waiting.SetActive (!internetNotReachable);
            text.SetActive (false);
        }

        public void onRewardedVideoFinished (double amount, string name) { }

        public void onRewardedVideoLoaded (bool precache)
        {
            NotInternet.SetActive (false);
            Waiting.SetActive (false);
            text.SetActive (true);
        }

        public void onRewardedVideoShown ()
        {
            if (_onVideShown != null) _onVideShown ();
        }
    }
}
