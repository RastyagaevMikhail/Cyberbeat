using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventSystemObject", menuName = "Events/GameCore/GameEvent/object")]
	public class GameEventSystemObject : ScriptableObject
	{
		protected List<EventListenerSystemObject> eventListeners = new List<EventListenerSystemObject> ();
		public void Raise (Object obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerSystemObject listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerSystemObject listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField] UnityEngine.Object testArg;
		[ContextMenu ("Trest Raise")] public void TestRaise () { Raise (testArg); }
	}
}
