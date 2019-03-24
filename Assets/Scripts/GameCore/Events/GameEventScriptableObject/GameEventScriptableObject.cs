
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventScriptableObject", menuName = "GameCore/GameEvent/UnityEngine/ScriptableObject")]
	public class GameEventScriptableObject : ScriptableObject
	{
		protected List<EventListenerScriptableObject> eventListeners = new List<EventListenerScriptableObject> ();
		public void Raise (ScriptableObject obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerScriptableObject listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerScriptableObject listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField] ScriptableObject testArg;
		[ContextMenu("Trest Raise")] public void TestRaise () { Raise (testArg); }
	}
}
