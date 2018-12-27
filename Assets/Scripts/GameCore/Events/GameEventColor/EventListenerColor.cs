using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerColor : EventListenerStruct<Color>
    {
        [SerializeField] GameEventColor EventObject;

        [SerializeField] UnityEventColor Responce;
        public EventListenerColor (GameEventColor _evnet, UnityAction<Color> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventColor ();
            Responce.AddListener (action);
        }

        public override void OnEventRaised (Color obj)
        {
            Responce.Invoke (obj);
        }

    }
}
