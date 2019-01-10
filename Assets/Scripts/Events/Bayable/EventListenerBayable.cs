using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	[System.Serializable] public class EventListenerBayable : AEventListenerUnityObject<ABayable>
	{
		[SerializeField] GameEventBayable _Event;
		public override AGameEventUnityObject<ABayable> Event { get { return _Event; } set { _Event = value as GameEventBayable; } }

		[SerializeField] UnityEventBayable responce;
		public override UnityEvent<ABayable> Responce { get { return responce; } }
		public EventListenerBayable (GameEventBayable _evnet, UnityAction<ABayable> action)
		{
			Event = _evnet;
			responce = new UnityEventBayable ();
			Responce.AddListener (action);
		}
	}

}
