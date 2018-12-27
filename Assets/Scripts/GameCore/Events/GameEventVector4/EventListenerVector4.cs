using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerVector4 : EventListenerStruct<Vector4>
    {
        [SerializeField] GameEventVector4 EventObject;

        [SerializeField] UnityEventVector4 Responce;
        public EventListenerVector4 (GameEventVector4 _evnet, UnityAction<Vector4> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventVector4 ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Vector4 obj)
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
