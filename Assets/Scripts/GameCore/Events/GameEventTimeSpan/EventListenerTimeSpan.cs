using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerTimeSpan : EventListenerStruct<TimeSpan>
    {
        [SerializeField] GameEventTimeSpan EventObject;
        protected override GameEventStruct<TimeSpan> AEventObject { get { return EventObject; } }
        [SerializeField] UnityEventTimeSpan Responce;
        protected override UnityEvent<TimeSpan> AResponce { get { return Responce; } }
        public EventListenerTimeSpan (GameEventTimeSpan _evnet, UnityAction<TimeSpan> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventTimeSpan ();
            Responce.AddListener (action);
        }

        public override void OnEventRaised (TimeSpan obj)
        {
            Responce.Invoke (obj);
        }
    }
}
