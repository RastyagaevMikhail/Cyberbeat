using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerGameObject
    {
        [SerializeField] GameEventGameObject EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventGameObject Responce;
        public EventListenerGameObject (GameEventGameObject _evnet, UnityAction<GameObject> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventGameObject ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (GameObject obj)
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
