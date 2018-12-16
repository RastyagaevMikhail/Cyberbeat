using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerUnityObject
    {
        [SerializeField] GameEventUnityObject EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventUnityObject Responce;
        public EventListenerUnityObject (GameEventUnityObject _evnet, UnityAction<UnityEngine.Object> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventUnityObject ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (UnityEngine.Object obj)
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
