namespace CyberBeat
{
    using GameCore;

    using UnityEngine.UI;
    using UnityEngine;
    using Text = TMPro.TextMeshProUGUI;
    using System;
    using UnityEngine.Events;

    public class GiftPopup : MonoBehaviour
    {
        [SerializeField] Image Icon;
        [SerializeField] Text CountText;
        [SerializeField] Text TitleText;
        [SerializeField] UnityEvent onRewardTaked;
        int reward;
        public RewardData rewardData { get { return RewardData.instance; } }
        IntVariable rewardVaribale;
        public void Init ()
        {
            rewardData.InitReward (out rewardVaribale, Icon, TitleText, out reward);

            CountText.text = "+{0}".AsFormat (reward);

        }   
        public void Take ()
        {
            rewardVaribale += reward;
            onRewardTaked.Invoke ();
        }
    }
}
