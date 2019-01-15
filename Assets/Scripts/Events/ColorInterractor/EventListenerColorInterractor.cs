using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	[System.Serializable] public class EventListenerColorInterractor : AEventListenerUnityObject<ColorInterractor,EventListenerColorInterractor>
	{
		[SerializeField] GameEventColorInterractor _Event;
		AGameEventUnityObject<ColorInterractor, EventListenerColorInterractor> evnt;
		public override AGameEventUnityObject<ColorInterractor, EventListenerColorInterractor> Event
		{
			get
			{
                GameEventColorInterractor _Event1 = _Event;
                return _Event1;
			}
		}

		[SerializeField] UnityEventColorInterractor responce;
		public override UnityEvent<ColorInterractor> Responce { get { return responce; } }

		public EventListenerColorInterractor (GameEventColorInterractor _evnet, UnityAction<ColorInterractor> action)
		{
			_Event = _evnet;
			responce = new UnityEventColorInterractor ();
			Responce.AddListener (action);
		}
	}

}
