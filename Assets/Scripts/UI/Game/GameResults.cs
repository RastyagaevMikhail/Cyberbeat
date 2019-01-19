﻿using GameCore;

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
        [SerializeField] Text ProgressText;
        [SerializeField] GameEvent ToMenu;
        [SerializeField] ResultsTrackInfo trackInfo;
        Track track { get { return TracksCollection.instance.CurrentTrack; } }

        public ResultsData data { get { return ResultsData.instance; } }
        public AdsController Ads { get { return AdsController.instance; } }

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
            var progress = track.progressInfo;
            ProgressText.text = "{0}/{1}".AsFormat (progress.Best.Value, progress.Max.Value);
            
            trackInfo.Init (track.music);
        }

        [ContextMenu ("Close")]
        public void Close ()
        {
            Back.SetActive (false);
        }

    }

}
