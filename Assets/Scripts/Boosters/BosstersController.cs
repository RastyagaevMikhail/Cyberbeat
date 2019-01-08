using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class BosstersController : TransformObject
	{
		public BoostersData data { get { return BoostersData.instance; } }
		private void Awake() {
			data.DeactivateAllBoosters();	
		}
		// public void OnPlayeDeath()
		// {
		// 	data.DeactivateAllBoosters();
		// }
	}
}
