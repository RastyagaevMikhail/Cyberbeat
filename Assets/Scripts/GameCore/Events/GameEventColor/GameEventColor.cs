using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventColor", menuName = "GameCore/GameEvent/UnityEngine/Color")]
	public class GameEventColor : ScriptableObject
	{
		[SerializeField] List<EventListenerColor> eventListeners = new List<EventListenerColor> ();
		public void Raise (Color color)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (color);
		}
		public void RegisterListener (EventListenerColor listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public void UnRegisterListener (EventListenerColor listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
