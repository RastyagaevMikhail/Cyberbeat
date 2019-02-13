using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[Serializable]
	public class EventListenerFloat
	{
		[SerializeField] GameEventFloat Event;

		[SerializeField] UnityEventFloat Responce;
		[SerializeField] bool debug;

		public EventListenerFloat (GameEventFloat _event, UnityAction<float> action)
		{
			Event = _event;
			Responce = new UnityEventFloat ();
			Responce.AddListener (action);
		}
		public void OnEventRaised (float arg)
		{
			Responce.Invoke (arg);
			if (debug) Debug.Log ($"{("OnEvent".a())} {Event.name.so()} {("Raised").a()}\n{Responce.Log()}");
		}
		public bool OnEnable ()
		{
			if (Event)
				Event.RegisterListener (this);
			return Event;

		}

		public bool OnDisable ()
		{
			if (Event)
				Event.UnRegisterListener (this);
			return Event;
		}

	}
}
