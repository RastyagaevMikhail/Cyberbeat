using GameCore;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (OnTimeEventVariableChanged))]
	public abstract class TimeEventsCatcher : SerializedMonoBehaviour
	{
		public abstract void _OnChanged (TimeEvent timeEvent);
	}
}
