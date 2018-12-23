using GameCore;

using Sirenix.OdinInspector;

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
        [SerializeField] GameObject Panel;
        [SerializeField] GameObject RewardButtons;
        [SerializeField] GameObject PostGetReawardButtons;
        [SerializeField] Button DoubleReward;
        [SerializeField] Button GetReward;
        [SerializeField] GameEvent ToMenu;

        public AdsController Ads { get { return AdsController.instance; } }
        private void OnValidate ()
        {
            Panel = transform.GetChild (0).gameObject;
            RewardButtons = Panel.transform.Find ("RewardButtons").gameObject;
            PostGetReawardButtons = Panel.transform.Find ("PostGetReawardButtons").gameObject;

            DoubleReward = RewardButtons.transform.Find ("DoubleRewardButton").GetComponent<Button> ();
            GetReward = RewardButtons.transform.Find ("GetRewardButton").GetComponent<Button> ();
            ToMenu = Tools.GetAssetAtPath<GameEvent> ("Assets/Data/Events/GameResults/To Menu.asset");
        }
        private void Awake ()
        {
            DoubleReward.onClick.RemoveAllListeners ();
            DoubleReward.onClick.AddListener (ShowVideo);

            GetReward.onClick.RemoveAllListeners ();
            GetReward.onClick.AddListener (SkipVideo);

        }
        private void ShowVideo ()
        {
            Ads.ShowRewardVideo ((a, n) => ShowRewardsButtons (false));
        }

        private void ShowRewardsButtons (bool Show)
        {
            RewardButtons.SetActive (Show);
            PostGetReawardButtons.SetActive (!Show);
        }

        private void SkipVideo ()
        {
            ShowRewardsButtons (false);
        }

        [Button]
        public void Show ()
        {
            Panel.SetActive (true);
            ShowRewardsButtons (true);
        }

        [Button]
        public void Close ()
        {
            Panel.SetActive (false);
        }

    }

}
