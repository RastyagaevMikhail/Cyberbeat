using GameCore;

using System;
using UnityEngine;

namespace CyberBeat
{
	[System.Serializable]
	public class ShopInfo
	{
		public int Price = 2000;
		public string SaveKey;
		public bool Buyed { get { return Tools.GetBool (SaveKey, Price == 0); } set { Tools.SetBool (SaveKey, value); } }
		
		public bool TryBuy ()
		{
			Buyed = Buyer.TryBuyDefaultCurency (Price);
			return Buyed;
		}

		public void ResetDefault ()
		{
			Buyed = (Price == 0);
		}

		public void Validate (string TrackName)
		{
			SaveKey = $"{TrackName} Buyed";
		}
	}
}
