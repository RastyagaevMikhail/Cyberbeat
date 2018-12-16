using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerTransform
    {
        [SerializeField] GameEventTransform EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventTransform Responce;
        public EventListenerTransform (GameEventTransform _evnet, UnityAction<Transform> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventTransform ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Transform obj)
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
