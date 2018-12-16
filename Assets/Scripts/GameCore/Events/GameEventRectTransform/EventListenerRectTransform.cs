using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] public class EventListenerRectTransform
    {
        [SerializeField] GameEventRectTransform EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEventRectTransform Responce;
        public EventListenerRectTransform (GameEventRectTransform _evnet, UnityAction<RectTransform> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventRectTransform ();
            Responce.AddListener (action);
        }

        public void OnEventRaised (RectTransform obj)
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
