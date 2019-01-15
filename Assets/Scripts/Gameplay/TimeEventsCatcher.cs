using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventListenerTimeEvent))]
	public abstract class TimeEventsCatcher : MonoBehaviour
	{
		public abstract void _OnChanged (TimeEvent timeEvent);
	}
}
