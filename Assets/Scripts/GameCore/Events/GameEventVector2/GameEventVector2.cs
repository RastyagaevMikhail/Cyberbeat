using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventVector2", menuName = "GameCore/GameEvent/UnityEngine/Vector2")]
	public class GameEventVector2 : GameEventStruct<Vector2>
	{
		protected List<EventListenerVector2> eventListeners = new List<EventListenerVector2> ();

		protected override List<EventListenerStruct<Vector2>> AEventListeners { get { return eventListeners.Cast<EventListenerStruct<Vector2>> ().ToList (); } }

		public override void Raise (Vector2 obj)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised (obj);
		}
		public virtual void RegisterListener (EventListenerVector2 listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public virtual void UnRegisterListener (EventListenerVector2 listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

	}
}
