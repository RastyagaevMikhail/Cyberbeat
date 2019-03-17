using GameCore;

using System;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "RewardVideoCondition", menuName = "GameCore/EventCondition/RewardVideo")]
	public class RewardVideoEventCondition : MonoBehaviour
	{
		[SerializeField] string nameReward;
		[SerializeField] UnityEventRewardVideo OnCorrect;
		public void DoCondiiton (double amount, string rewardName)
		{
			if (rewardName.Equals (nameReward))
				OnCorrect.Invoke (amount, rewardName);
		}
	}

}
