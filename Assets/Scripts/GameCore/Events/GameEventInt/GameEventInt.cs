using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventInt", menuName = "Events/GameCore/GameEvent/int")]
	public class GameEventInt : ScriptableObject
	{
		List<EventListenerInt> eventListeners = new List<EventListenerInt> ();
		public void Raise (int arg)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (arg);
		}

		public virtual void RegisterListener (EventListenerInt listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerInt listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}
	}
}
