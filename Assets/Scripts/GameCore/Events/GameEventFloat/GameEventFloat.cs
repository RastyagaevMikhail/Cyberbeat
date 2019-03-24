using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventFloat", menuName = "GameCore/GameEvent/System/Float")]
	public class GameEventFloat : ScriptableObject
	{
		[SerializeField] List<EventListenerFloat> eventListeners = new List<EventListenerFloat> ();
		public void Raise (float arg)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (arg);
		}

		public virtual void RegisterListener (EventListenerFloat listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerFloat listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}
	}
}
