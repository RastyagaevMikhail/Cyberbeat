using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [System.Serializable]
    public abstract class AEventListenerUnityObject<TObject>
        where TObject : UnityEngine.Object
        {
            public abstract AGameEventUnityObject<TObject> Event { get; set; }
            public abstract UnityEvent<TObject> Responce { get; }
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
