using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[Serializable]
	public class EventListenerInt
	{
		[SerializeField] GameEventInt Event;

		[DrawWithUnity]
		[SerializeField] UnityEventInt Responce;

		public EventListenerInt (GameEventInt _event, UnityAction<int> action)
		{
			Event = _event;
			Responce = new UnityEventInt ();
			Responce.AddListener (action);
		}
		public void OnEventRaised (int arg)
		{
			Responce.Invoke (arg);
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
