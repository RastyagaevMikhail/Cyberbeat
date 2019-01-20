using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent(typeof(GameEventTimeEvent))]
	public abstract class TimeEventsCatcher : MonoBehaviour
	{
		public abstract void _OnChanged (TimeEvent timeEvent);
	}
}
