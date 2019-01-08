using System.Collections.Generic;

using UnityEngine;
using System;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventSystemAction", menuName = "Events/GameCore/GameEvent/System.Action")]
	public class GameEventSystemAction : ScriptableObject
	{
		protected List<EventListenerSystemAction> eventListeners = new List<EventListenerSystemAction> ();
		public void Raise (Action obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerSystemAction listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerSystemAction listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}
	}
}
