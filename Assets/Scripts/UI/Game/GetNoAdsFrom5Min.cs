using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
    public class GetNoAdsFrom5Min : MonoBehaviour
    {
        [SerializeField] Button WatchButton;
        [SerializeField] Button SkipButton;
        [SerializeField] GameObject Back;
        [SerializeField] BoolVariable NoAdsAutoRestartTime;
        [SerializeField] TimeSpanVariable NoAdsTime;
        [SerializeField] GameEvent ShowResults;
        [SerializeField] GameEvent RestartGame;

        public AdsController Ads { get { return AdsController.instance; } }
#if UNITY_EDITOR
        private void OnValidate ()
        {
            Back = transform.GetChild (0).gameObject;

            var Panel = Back.transform.GetChild (0);

            WatchButton = Panel.transform.Find ("GetAndWatch").GetComponent<Button> ();
            SkipButton = Panel.transform.Find ("Skip").GetComponent<Button> ();

            NoAdsAutoRestartTime = Tools.ValidateVaraiable<BoolVariable> ("Assets/Data/NoAdsTimer/NoAdsAutoRestartTime.asset");
            NoAdsTime = Tools.ValidateVaraiable<TimeSpanVariable> ("Assets/Data/NoAdsTimer/NoAdsTime.asset");

            ShowResults = Tools.ValidateSO<GameEvent> ("Assets/Data/Events/GetNoAdsFrom5Min/Show Results.asset");
            RestartGame = Tools.ValidateSO<GameEvent> ("Assets/Data/Events/GameController/OnRestartGame.asset");
        }
#endif

        [ContextMenu ("Show")]
        public void Show ()
        {
            Back.SetActive (true);
        }

        [ContextMenu ("Close")]
        public void Close ()
        {
            Back.SetActive (false);
        }

        private void Awake ()
        {
            WatchButton.onClick.RemoveAllListeners ();
            WatchButton.onClick.AddListener (ShowVideo);

            SkipButton.onClick.RemoveAllListeners ();
            SkipButton.onClick.AddListener (SkipVideo);
        }
        private void ShowVideo ()
        {
            Ads.ShowRewardVideo (ResetNoAdsTimer);

            RestartGame.Raise ();

            Close ();
        }

        private void ResetNoAdsTimer (double arg1, string arg2)
        {
            NoAdsAutoRestartTime.Value = true;
            
            NoAdsTime.ResetDefault ();
        }

        public void SkipVideo ()
        {
            ShowResults.Raise ();

            Close ();
        }
    }
}
