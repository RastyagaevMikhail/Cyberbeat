using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerSystemAction
    {
        [SerializeField] GameEventSystemAction EventObject;

        [SerializeField] UnityEventSystemAction Responce;
        public EventListenerSystemAction (GameEventSystemAction _evnet, UnityAction<Action> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventSystemAction ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Action obj)
        {
            Responce.Invoke (obj);
        }
        public bool OnEnable ()
        {
            if (EventObject)
                EventObject.RegisterListener (this);
            return EventObject;

        }

        public bool OnDisable ()
        {
            if (EventObject)
                EventObject.UnRegisterListener (this);
            return EventObject;
        }
    }
}
