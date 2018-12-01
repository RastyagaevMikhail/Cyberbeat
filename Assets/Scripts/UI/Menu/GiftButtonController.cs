using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class GiftButtonController : RectTransformObject
	{
#if UNITY_EDITOR
		private void Awake ()
		{
			x = -70;
		}
#endif
		public AdsController Ads { get { return AdsController.instance; } }

		[SerializeField] GiftPopup giftPopup;

		public void ShowGift ()
		{
			Ads.ShowRewardVideo ((a, n) => giftPopup.Show ());
		}
	}
}
