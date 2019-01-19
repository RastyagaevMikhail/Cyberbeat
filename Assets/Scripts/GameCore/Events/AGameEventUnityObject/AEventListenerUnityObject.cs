using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [System.Serializable]
    public abstract class AEventListenerUnityObject<TObject>
        where TObject : UnityEngine.Object
<<<<<<< HEAD
    where TEventListener : AEventListenerUnityObject<TObject, TEventListener>
    {
        public abstract AGameEventUnityObject<TObject, TEventListener> Event { get; }

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
=======
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
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory

        }

<<<<<<< HEAD
        public bool OnDisable ()
        {
            if (Event)
                Event.UnRegisterListener (this as TEventListener);
            return Event;
=======
            public bool OnDisable ()
            {
                if (Event)
                    Event.UnRegisterListener (this);
                return Event;
            }
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory
        }
    }
}
