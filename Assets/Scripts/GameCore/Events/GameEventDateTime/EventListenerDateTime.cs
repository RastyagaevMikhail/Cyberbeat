using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerDateTime : EventListenerStruct<DateTime>
    {
        [SerializeField] GameEventDateTime EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventDateTime Responce;
        public EventListenerDateTime (GameEventDateTime _evnet, UnityAction<DateTime> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventDateTime ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (DateTime obj)
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
