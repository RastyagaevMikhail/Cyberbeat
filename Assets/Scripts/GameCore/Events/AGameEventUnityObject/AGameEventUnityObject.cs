using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public abstract class AGameEventUnityObject<TObject, TEventListener> : ScriptableObject
	where TObject : UnityEngine.Object
	where TEventListener : AEventListenerUnityObject<TObject, TEventListener>
	{
		[SerializeField]
		abstract protected List<TEventListener> eventListeners { get; set; }
		public void Raise (TObject obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (TEventListener listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (TEventListener listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField] TObject testArg;
		[ContextMenu ("Test Raise")] public void TestRaise () { Raise (testArg); }
	}
}
