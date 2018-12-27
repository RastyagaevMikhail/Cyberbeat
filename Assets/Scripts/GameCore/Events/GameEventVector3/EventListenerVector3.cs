using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerVector3 : EventListenerStruct<Vector3>
    {
        [SerializeField] GameEventVector3 EventObject;

        [SerializeField] UnityEventVector3 Responce;
        public EventListenerVector3 (GameEventVector3 _evnet, UnityAction<Vector3> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventVector3 ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Vector3 obj)
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
