using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerScriptableObject
    {
        [SerializeField] GameEventScriptableObject EventObject;

        [SerializeField] UnityEventScriptableObject Responce;
        public EventListenerScriptableObject (GameEventScriptableObject _evnet, UnityAction<ScriptableObject> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventScriptableObject ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (ScriptableObject obj)
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
