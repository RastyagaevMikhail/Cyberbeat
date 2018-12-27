using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerTimeSpan : EventListenerStruct<TimeSpan>
    {
        [SerializeField] GameEventTimeSpan EventObject;

        [SerializeField] UnityEventTimeSpan Responce;
        public EventListenerTimeSpan (GameEventTimeSpan _evnet, UnityAction<TimeSpan> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventTimeSpan ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (TimeSpan obj)
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
