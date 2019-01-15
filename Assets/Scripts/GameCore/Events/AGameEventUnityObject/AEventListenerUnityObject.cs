using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [System.Serializable]
    public abstract class AEventListenerUnityObject<TObject, TEventListener>
        where TObject : UnityEngine.Object
    where TEventListener : AEventListenerUnityObject<TObject, TEventListener>

        // where TGameEvent : AGameEventUnityObject<TObject, AEventListenerUnityObject<TObject>>

        {
            public abstract AGameEventUnityObject<TObject, TEventListener> Event { get; }
            // public abstract AGameEventUnityObject<TObject, TEventListener> Event { get; }
            public abstract UnityEvent<TObject> Responce { get; }
            public void OnEventRaised (TObject obj)
            {
                Responce.Invoke (obj);
            }
            public bool OnEnable ()
            {
                if (Event)
                    Event.RegisterListener (this as TEventListener);
                return Event;

            }

            public bool OnDisable ()
            {
                if (Event)
                    Event.UnRegisterListener (this as TEventListener);
                return Event;
            }
        }
}
