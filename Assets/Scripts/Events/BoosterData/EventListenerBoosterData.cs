using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	[System.Serializable] public class EventListenerBoosterData : AEventListenerUnityObject<BoosterData>
	{
		[SerializeField] GameEventBoosterData _Event;
		public override AGameEventUnityObject<BoosterData> Event { get { return _Event; } set { _Event = value as GameEventBoosterData; } }

		[SerializeField] UnityEventBoosterData responce;
		public override UnityEvent<BoosterData> Responce { get { return responce; } }
		public EventListenerBoosterData (GameEventBoosterData _evnet, UnityAction<BoosterData> action)
		{
			Event = _evnet;
			responce = new UnityEventBoosterData ();
			Responce.AddListener (action);
		}
	}

}
