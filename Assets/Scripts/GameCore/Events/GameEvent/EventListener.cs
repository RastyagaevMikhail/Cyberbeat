using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListener
    {
        [SerializeField] GameEvent Event;

        [DrawWithUnity]
        [SerializeField] UnityEvent Responce;
        public EventListener (GameEvent startEvent, UnityAction action)
        {
            Event = startEvent;
            Responce = new UnityEvent ();
            Responce.AddListener (action);

        }

        public void OnEventRaised ()
        {
            Responce.Invoke ();
        }
        public bool OnEnable ()
        {
            bool IfNotNullEvent = Event != null;
            if (IfNotNullEvent)
                Event.RegisterListener (this);
            return IfNotNullEvent;

        }

        public bool OnDisable ()
        {
            bool IfNotNullEvent = Event != null;
            if (IfNotNullEvent)
                Event.UnRegisterListener (this);
            return IfNotNullEvent;
        }
    }
}

