using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CyberBeat
{
    public class ButtonActionByVideoAds : MonoBehaviour
    {

        public AdsController adsController { get { return AdsController.instance; } }
        private Button _button = null;
        public Button button { get { if (_button == null) _button = GetComponent<Button> (); return _button; } }

        [SerializeField] GameObject Content;
        [SerializeField] GameObject NotInternet;
        [SerializeField] GameObject Waiting;
        private bool isLoaded { get { return adsController.isLoadedRewardVideo; } }
        bool internetNotReachable { get { return adsController.internetNotReachable; } }
        private void Start ()
        {
            adsController.OnRewardedVideoLoaded += OnRewardedVideoLoaded;
            OnButtonVideoShown += UpdateSatate;
            UpdateSatate ();
        }

        private void OnDestroy ()
        {
            OnButtonVideoShown -= UpdateSatate;
            adsController.OnRewardedVideoLoaded -= OnRewardedVideoLoaded;
        }
        private void OnRewardedVideoLoaded (bool precache)
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
        Action OnButtonVideoShown;
        Action _onVideShown;
        public void Init (Action OnVideoShown)
        {
            _onVideShown = OnVideoShown;
        }
        public void ShowVideo ()
        {
            adsController.ShowRewardVideo ((amount, name) =>
            {
                if (_onVideShown != null) _onVideShown ();
            });
            if (OnButtonVideoShown != null) OnButtonVideoShown ();
        }
    }
}
