
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventDateTime", menuName = "Events/GameCore/GameEvent/DateTime")]
	public class GameEventDateTime : GameEventStruct<DateTime>
	{
		protected List<EventListenerDateTime> eventListeners = new List<EventListenerDateTime> ();
		protected override List<EventListenerStruct<DateTime>> AEventListeners { get { return eventListeners.Cast<EventListenerStruct<DateTime>> ().ToList (); } }
		// public void Raise (DateTime obj)
		// {
		// 	for (int i = eventListeners.Count - 1; i >= 0; i--)
		// 		eventListeners[i].OnEventRaised (obj);
		// }
		public virtual void RegisterListener (EventListenerDateTime listener)
		{
			if (!AEventListeners.Contains (listener))
				AEventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerDateTime listener)
		{
			if (AEventListeners.Contains (listener))
				AEventListeners.Remove (listener);
		}

	}
}
