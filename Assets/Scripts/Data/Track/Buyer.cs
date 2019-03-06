using GameCore;

using UnityEngine;

namespace CyberBeat
{
	[CreateAssetMenu (menuName = "CyberBeat/Buyer")]
	public class Buyer : ScriptableObject
	{
		private static IntVariable _defaultCurency;
		static IntVariable defaultCurency => _defaultCurency??(_defaultCurency = Resources.Load<IntVariable> ("Data/Variables/GameData/Notes"));
		public static bool TryBuyDefaultCurency (int price)
		{
			bool canBuy = (defaultCurency.Value - price >= 0);
			if (canBuy)
				defaultCurency.ApplyChange (-price);
			return canBuy;
		}

		[SerializeField] IntVariable curency;
		public bool TryBuy (int price)
		{
			bool canBuy = (curency.Value - price >= 0);
			if (canBuy)
				curency.ApplyChange (-price);
			return canBuy;
		}
	}
}
