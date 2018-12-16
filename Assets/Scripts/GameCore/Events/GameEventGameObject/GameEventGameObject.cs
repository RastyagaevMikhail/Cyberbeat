using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventGameObject", menuName = "Events/GameCore/GameEvent/GameObject")]
	public class GameEventGameObject : ScriptableObject
	{
		protected List<EventListenerGameObject> eventListeners = new List<EventListenerGameObject> ();
		public void Raise (GameObject obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerGameObject listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerGameObject listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[Title ("Test")]
		[SerializeField] GameObject testArg;
		[Button] public void TestRaise () { Raise (testArg); }
	}
}
