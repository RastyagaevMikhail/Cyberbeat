using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using  GameCore;
namespace CyberBeat
{
	public class RuntimeTimerView : MonoBehaviour
	{
		public Slider slider;
		public Image Icon;
		RuntimeTimer currenttimer;
		public void Init (RuntimeTimer timer, Sprite sprite = null)
		{
			Icon.sprite = sprite;
			gameObject.SetActive (true);

			currenttimer = timer;
			currenttimer.OnTimeProgress += ProcessSlider;
			currenttimer.OnTimeElapsed += TimeElapsed;
		}

		private void TimeElapsed ()
		{
			currenttimer.OnTimeProgress -= ProcessSlider;
			currenttimer.OnTimeElapsed -= TimeElapsed;
			gameObject.SetActive (false);
		}

		private void ProcessSlider (float percent)
		{
			slider.value = percent;
		}
	}
}
