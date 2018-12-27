
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventObject", menuName = "Events/GameCore/GameEvent/Object")]
	public abstract class AGameEventUnityObject<TObject> : ScriptableObject
	where TObject : UnityEngine.Object
	{
		protected List<AEventListenerUnityObject<TObject>> eventListeners = new List<AEventListenerUnityObject<TObject>> ();
		public void Raise (TObject obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (AEventListenerUnityObject<TObject> listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (AEventListenerUnityObject<TObject> listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[SerializeField] TObject testArg;
		[ContextMenu("Test Raise")] public void TestRaise () { Raise (testArg); }
	}
}
