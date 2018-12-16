using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventScriptableObject", menuName = "Events/GameCore/GameEvent/ScriptableObject")]
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

		[Title ("Test")]
		[SerializeField] ScriptableObject testArg;
		[Button] public void TestRaise () { Raise (testArg); }
	}
}
