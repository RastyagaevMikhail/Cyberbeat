using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerColor : EventListenerStruct<Color>
    {
        [SerializeField] GameEventColor EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventColor Responce;
        public EventListenerColor (GameEventColor _evnet, UnityAction<Color> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventColor ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Color obj)
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
