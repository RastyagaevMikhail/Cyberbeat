using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventTransform", menuName = "GameCore/GameEvent/UnityEngine/Transform")]
	public class GameEventTransform : ScriptableObject
	{
		protected List<EventListenerTransform> eventListeners = new List<EventListenerTransform> ();
		public void Raise (Transform obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerTransform listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerTransform listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField] Transform testArg;
		[ContextMenu ("Test Raise")] public void TestRaise () { Raise (testArg); }
	}
}
