using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventObject", menuName = "Events/GameCore/GameEvent/Object")]
	public class GameEventUnityObject : ScriptableObject
	{
		protected List<EventListenerUnityObject> eventListeners = new List<EventListenerUnityObject> ();
		public UnityObjectVariable arg;
		public void Raise (Object obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerUnityObject listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerUnityObject listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField] UnityEngine.Object testArg;
		[ContextMenu("Test Raise")] public void TestRaise () { Raise (testArg); }
	}
}
