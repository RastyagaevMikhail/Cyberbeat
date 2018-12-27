using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerDateTime : EventListenerStruct<DateTime>
    {
        [SerializeField] GameEventDateTime EventObject;

        [SerializeField] UnityEventDateTime Responce;
        public EventListenerDateTime (GameEventDateTime _evnet, UnityAction<DateTime> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventDateTime ();
            Responce.AddListener (action);
        }

        public override void OnEventRaised (DateTime obj)
        {
            Responce.Invoke (obj);
        }
    }
}
