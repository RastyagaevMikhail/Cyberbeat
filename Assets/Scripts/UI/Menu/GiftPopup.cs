namespace CyberBeat
{
    using GameCore;

    using UnityEngine.UI;
    using UnityEngine;
    using Text = TMPro.TextMeshProUGUI;
    using System;

    public class GiftPopup : MonoBehaviour
    {
        [SerializeField] Image Icon;
        [SerializeField] Text CountText;
        [SerializeField] Text TitleText;
        [SerializeField] GameEvent OnRewardTaked;
        [SerializeField] DateTimeVariable LastDateTimeRewardTaked;
        int reward;
        public RewardData rewardData { get { return RewardData.instance; } }
        IntVariable rewardVaribale;
        public void Show ()
        {
            rewardData.InitReward (out rewardVaribale, Icon, TitleText, out reward);
            CountText.text = "+{0}".AsFormat (reward);
            gameObject.SetActive (true);
        }

        public void Close ()
        {
            gameObject.SetActive (false);
        }
        public void Take ()
        {
            Close ();
            rewardVaribale += reward;
            OnRewardTaked.Raise ();
            LastDateTimeRewardTaked.Value = DateTime.Now;
        }
    }
}
