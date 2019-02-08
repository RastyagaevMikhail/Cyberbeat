using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventBool", menuName = "Events/GameCore/GameEvent/bool")]
	public class GameEventBool : ScriptableObject
	{
		List<EventListenerBool> eventListeners = new List<EventListenerBool> ();
		public void Raise (bool obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}

		public virtual void RegisterListener (EventListenerBool listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerBool listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField, DisableInEditorMode] bool testArg;
		[Button, DisableInEditorMode] public void Raise () => Raise (testArg);
	}
}
