using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerVector2 : EventListenerStruct<Vector2>
    {
        [SerializeField] GameEventVector2 EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventVector2 Responce;
        public EventListenerVector2 (GameEventVector2 _evnet, UnityAction<Vector2> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventVector2 ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (Vector2 obj)
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
