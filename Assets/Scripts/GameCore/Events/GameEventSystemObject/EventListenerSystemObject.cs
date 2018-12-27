using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerSystemObject
    {
        [SerializeField] GameEventSystemObject EventObject;

        [SerializeField] UnityEventSystemObject Responce;
        public EventListenerSystemObject (GameEventSystemObject _evnet, UnityAction<System.Object> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventSystemObject ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (System.Object obj)
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
