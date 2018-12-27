using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerMaterial
    {
        [SerializeField] GameEventMaterial EventObject;

        [SerializeField] UnityEventMaterial Responce;
        public EventListenerMaterial (GameEventMaterial _evnet, UnityAction<Material> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventMaterial ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Material obj)
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
