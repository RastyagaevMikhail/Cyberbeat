using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "TimeEvent", menuName = "Variables/CyberBeat/TimeEvent")]
	public class TimeEventVariable : SavableVariable<TimeEvent>
	{
#if UNITY_EDITOR
		public override void ResetDefault () { }

#endif
		public override void SaveValue () { }

		public TimeEventVariable SetValue (TimeEvent timeEvent)
		{
			Value = timeEvent;
			return this;
		}
	}
}
