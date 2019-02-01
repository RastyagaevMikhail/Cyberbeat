using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{

    public class GameResults : MonoBehaviour
    {
        [SerializeField] GameObject Back;
        [SerializeField] GameObject RewardButtons;
        [SerializeField] GameObject PostGetReawardButtons;
        [SerializeField] Button DoubleReward;
        [SerializeField] Button GetReward;
        [SerializeField] ResultsTrackInfo trackInfo;
        [SerializeField] TrackVariable trackVariable;
        Track track { get { return trackVariable.ValueFast; } }

        [SerializeField] ResultsData data;
        [SerializeField] AdsController Ads;

        private void Awake ()
        {
            DoubleReward.onClick.RemoveAllListeners ();
            DoubleReward.onClick.AddListener (ShowVideo);

            GetReward.onClick.RemoveAllListeners ();
            GetReward.onClick.AddListener (SkipVideo);

        }
        private void ShowVideo ()
        {
            Ads.ShowRewardVideo (GetDoubleReward);
        }

        void GetDoubleReward ()
        {
            ShowRewardsButtons (false);
            data.TakeDoubleReward ();
        }
        private void ShowRewardsButtons (bool Show)
        {
            RewardButtons.SetActive (Show);
            PostGetReawardButtons.SetActive (!Show);
        }

        private void SkipVideo ()
        {
            ShowRewardsButtons (false);
            data.TakeReward ();
        }

        [ContextMenu ("Show")]
        public void Show ()
        {
            Back.SetActive (true);
            ShowRewardsButtons (true);
            data.Calculate ();

            trackInfo.Init (track.music);
        }

        [ContextMenu ("Close")]
        public void Close ()
        {
            Back.SetActive (false);
        }

    }

}
