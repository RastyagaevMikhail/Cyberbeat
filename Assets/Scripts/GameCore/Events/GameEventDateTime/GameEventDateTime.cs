
using System;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventDateTime", menuName = "Events/GameCore/GameEvent/DateTime")]
	public class GameEventDateTime : GameEventStruct<DateTime>
	{
		// protected List<EventListenerDateTime> eventListeners = new List<EventListenerDateTime> ();
		// public void Raise (DateTime obj)
		// {
		// 	for (int i = eventListeners.Count - 1; i >= 0; i--)
		// 		eventListeners[i].OnEventRaised (obj);
		// }
		public virtual void RegisterListener (EventListenerDateTime listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerDateTime listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
