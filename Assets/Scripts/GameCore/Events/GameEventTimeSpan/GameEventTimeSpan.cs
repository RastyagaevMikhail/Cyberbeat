using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventTimeSpan", menuName = "Events/GameCore/GameEvent/TimeSpan")]
	public class GameEventTimeSpan : GameEventStruct<TimeSpan>
	{
		protected List<EventListenerTimeSpan> eventListeners = new List<EventListenerTimeSpan> ();
		protected override List<EventListenerStruct<TimeSpan>> AEventListeners { get { return eventListeners.Cast<EventListenerStruct<TimeSpan>> ().ToList (); } }

		// protected override List<EventListenerStruct<TStruct>> AEventListeners { get { return eventListeners; } }
		public override void Raise (TimeSpan obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerTimeSpan listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerTimeSpan listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
