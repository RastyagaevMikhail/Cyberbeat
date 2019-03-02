using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CyberBeat
{
    [RequireComponent(typeof(GameEventListernerBool))]
    public class ButtonActionByVideoAds : MonoBehaviour
    {

        [HideInInspector][SerializeField] AdsController adsController;
        [HideInInspector][SerializeField] Button button;

        [SerializeField] GameObject Content;
        [SerializeField] GameObject NotInternet;
        [SerializeField] GameObject Waiting;
        private bool isLoaded { get { return adsController.isLoadedRewardVideo; } }
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
        private void OnValidate ()
        {
            if (adsController == null) adsController = Resources.Load<AdsController> ("Data/AdsController");
            if (button == null) button = GetComponent<Button> ();
        }
        private void Start ()
        {
            UpdateSatate ();
        }
        public void OnRewardedVideoLoaded (bool precache)
        {
            UpdateSatate ();
        }
        private void UpdateSatate ()
        {
            // Debug.LogFormat ("UpdateSatate = {0}.{1}", name, transform.parent.parent.name);
            if (Content)
                Content.SetActive (isLoaded);
            NotInternet.SetActive (!isLoaded && internetNotReachable);
            Waiting.SetActive (!isLoaded && !internetNotReachable);
            button.interactable = isLoaded && !internetNotReachable;
        }
    }
}
