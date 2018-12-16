using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventMaterial", menuName = "Events/GameCore/GameEvent/Material")]
	public class GameEventMaterial : ScriptableObject
	{
		protected List<EventListenerMaterial> eventListeners = new List<EventListenerMaterial> ();
		public void Raise (Material obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerMaterial listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerMaterial listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[Title ("Test")]
		[SerializeField] Material testArg;
		[Button] public void TestRaise () { Raise (testArg); }
	}
}
