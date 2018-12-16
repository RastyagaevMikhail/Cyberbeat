using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventColor", menuName = "Events/GameCore/GameEvent/Color")]
	public class GameEventColor : GameEventStruct<Color>
	{
		protected List<EventListenerColor> eventListeners = new List<EventListenerColor> ();
		public void Raise (Color obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerColor listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerColor listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
