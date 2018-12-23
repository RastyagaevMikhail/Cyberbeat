using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	[System.Serializable] public class EventListenerColorInterractor : AEventListenerUnityObject<ColorInterractor>
	{
		[SerializeField] GameEventColorInterractor _Event;
		protected override AGameEventUnityObject<ColorInterractor> Event { get { return _Event; } set { _Event = value as GameEventColorInterractor; } }

		[SerializeField] UnityEventColorInterractor responce;
		protected override UnityEvent<ColorInterractor> Responce { get { return responce; } }
		public EventListenerColorInterractor (GameEventColorInterractor _evnet, UnityAction<ColorInterractor> action)
		{
			Event = _evnet;
			responce = new UnityEventColorInterractor ();
			Responce.AddListener (action);
		}
	}

}
