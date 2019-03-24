using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventVector3", menuName = "GameCore/GameEvent/UnityEngine/Vector3")]
	public class GameEventVector3 : GameEventStruct<Vector3>
	{
		protected List<EventListenerVector3> eventListeners = new List<EventListenerVector3> ();
		protected override List<EventListenerStruct<Vector3>> AEventListeners { get { return eventListeners.Cast<EventListenerStruct<Vector3>> ().ToList (); } }
		public override void Raise (Vector3 obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerVector3 listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerVector3 listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
