using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CyberBeat
{
	public class NoAdsMenuController : MonoBehaviour
	{
		private Button _button = null;
		public Button button { get { if (_button == null) _button = GetComponent<Button> (); return _button; } }
		private TimeSpanTimerAction _timer = null;
		public TimeSpanTimerAction timer { get { if (_timer == null) _timer = GetComponent<TimeSpanTimerAction> (); return _timer; } }
		public AdsController Ads { get { return AdsController.instance; } }

		[SerializeField] TimeSpanVariable NoAdsTime;
		[SerializeField] BoolVariable NoAdsIsEnabled;
		private void Awake ()
		{
			button.interactable = !NoAdsIsEnabled.Value;
		}
		public void _ResetNoAdsTime ()
		{
			Ads.ShowRewardVideo ((a, n) =>
			{
				NoAdsTime.Value = new System.TimeSpan (0, 5, 0);
				NoAdsTime.SaveValue ();
				NoAdsIsEnabled.Value = true;
				timer.StartTimer ();
				button.interactable = false;
			});

		}
	}
}
