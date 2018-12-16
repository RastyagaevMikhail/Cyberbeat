using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [System.Serializable]
    public abstract class AEventListenerUnityObject<TObject>
        where TObject : UnityEngine.Object
        {
            [SerializeField] protected abstract AGameEventUnityObject<TObject> Event { get; set; }
            [DrawWithUnity]
            [SerializeField] protected abstract UnityEvent<TObject> Responce { get; }
            // public EventListenerUnityObject (GameEventUnityObject _evnet, UnityAction<UnityEngine.Object> action)
            // {
            // EventObject = _evnet;
            //     Responce = new UnityEventUnityObject ();
            //     Responce.AddListener (action);
            // }

            public void OnEventRaised (TObject obj)
            {
                Responce.Invoke (obj);
            }
            public bool OnEnable ()
            {
                if (Event)
                    Event.RegisterListener (this);
                return Event;

            }

            public bool OnDisable ()
            {
                if (Event)
                    Event.UnRegisterListener (this);
                return Event;
            }
        }
}
