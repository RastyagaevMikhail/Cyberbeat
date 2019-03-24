using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventVector4", menuName = "GameCore/GameEvent/UnityEngine/Vector4")]
	public class GameEventVector4 : GameEventStruct<Vector4>
	{
		protected List<EventListenerVector4> eventListeners = new List<EventListenerVector4> ();
		protected override List<EventListenerStruct<Vector4>> AEventListeners { get { return eventListeners.Cast<EventListenerStruct<Vector4>> ().ToList (); } }
		public override void Raise (Vector4 obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerVector4 listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerVector4 listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
