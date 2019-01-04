using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public abstract class GameEventStruct<TStruct> : ScriptableObject where TStruct : struct
	{
		protected abstract List<EventListenerStruct<TStruct>> AEventListeners { get; }
		public virtual void Raise (TStruct obj)
		{
			for (int i = AEventListeners.Count - 1; i >= 0; i--)
				AEventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerStruct<TStruct> listener)
		{
			if (!AEventListeners.Contains (listener))
				AEventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerStruct<TStruct> listener)
		{
			if (AEventListeners.Contains (listener))
				AEventListeners.Remove (listener);
		}

	}
}
