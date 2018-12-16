using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventRectTransform", menuName = "Events/GameCore/GameEvent/RectTransform")]
	public class GameEventRectTransform : ScriptableObject
	{
		protected List<EventListenerRectTransform> eventListeners = new List<EventListenerRectTransform> ();
		public void Raise (RectTransform obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerRectTransform listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerRectTransform listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[Title ("Test")]
		[SerializeField] RectTransform testArg;
		[Button] public void TestRaise () { Raise (testArg); }
	}
}
