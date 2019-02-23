using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CyberBeat
{
	public class RewardData : SingletonData<RewardData>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/RewardData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

		public override void InitOnCreate () { }

		public override void ResetDefault () { }
#else
		public override void ResetDefault () { }
#endif
		[SerializeField] float rewardChnacePechent = 0.08f;
		[SerializeField] TimeSpan TimeFoemNextReward = new TimeSpan (3, 0, 0);
		[SerializeField] FloatVariable UserChance;
		[SerializeField] TimeSpanVariable RewardTime;
		[SerializeField] IntListVariable RewardIndexes;
		[SerializeField] DateTimeVariable LastDateTimeRewardTaked;
		public void InitReward (out IntVariable rewardVaribale, Image icon, TextMeshProUGUI titleText, out int reward)
		{
			if (!LastDateTimeRewardTaked.isNew)
			{
				var ts = DateTime.Now - LastDateTimeRewardTaked;
				int CountSkipedReward = 0;
				while (ts >= TimeFoemNextReward)
				{
					CountSkipedReward++;
					ts = ts - TimeFoemNextReward;
				}
				UserChance.Value += (CountSkipedReward == 0) ? -rewardChnacePechent : CountSkipedReward * rewardChnacePechent;
				UserChance.Clamp (0.3f, 1f);
			}

			var ri = rewards[RewardIndexes.random];

			rewardVaribale = ri.CountVarible;
			titleText.text = ri.LocTag.localized ();
			icon.sprite = ri.Icon;

			int max = (UserChance.Value * 10).RoundToInt () - 1;
			int min = 0;

			int rewardIndex = (UserChance.Value >= 0.8f) ? max : Random.Range (min, max + 1);

			reward = ri.CountsByChanche[rewardIndex];

		}

		public List<RewardInfo> rewards = new List<RewardInfo> ();
		[System.Serializable]
		public class RewardInfo
		{
			public string LocTag;
			public Sprite Icon;
			public IntVariable CountVarible;
			public List<int> CountsByChanche;
		}

		public void ResetRewardTime ()
		{
			RewardTime.Value = TimeFoemNextReward;
			RewardTime.SaveValue ();
		}
	}
}
