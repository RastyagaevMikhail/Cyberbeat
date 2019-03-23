using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[Serializable]
	public class EventListenerBool
	{
		[SerializeField] GameEventBool Event;
		[SerializeField] UnityEventBool Responce;
		[SerializeField] UnityEventBool ResponceInverce;
		[SerializeField] bool inverse;

		public EventListenerBool (GameEventBool _event, UnityAction<bool> action)
		{
			Event = _event;
			Responce = new UnityEventBool ();
			Responce.AddListener (action);
		}
		public void OnEventRaised (bool arg)
		{
			Responce.Invoke (inverse?!arg : arg);
			ResponceInverce.Invoke(!arg);
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
