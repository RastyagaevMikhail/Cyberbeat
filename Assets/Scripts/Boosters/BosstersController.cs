using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class BosstersController : TransformObject
	{
		[SerializeField] RuntimeTimerView timerView;
		BoostersData boostersData { get { return BoostersData.instance; } }
		Pool pool { get { return Pool.instance; } }

		public void OnBoosterActivated (BoosterData boosterData)
		{
			var timer = pool.Pop<RuntimeTimer> ("RuntimeTimer");
			
			timerView.Init (timer, boosterData.Icon);

			boosterData.InitTimer (timer);
		}

	}
}
