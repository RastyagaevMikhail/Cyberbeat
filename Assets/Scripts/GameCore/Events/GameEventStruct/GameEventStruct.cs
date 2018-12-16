using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{	
	public abstract class GameEventStruct<TStruct> : ScriptableObject where TStruct : struct
	{
		protected List<EventListenerStruct<TStruct>> eventListeners = new List<EventListenerStruct<TStruct>> ();
		public void Raise (TStruct obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerStruct<TStruct> listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerStruct<TStruct> listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
