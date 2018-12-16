using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable] 
    public abstract class EventListenerStruct<TStruct> where TStruct : struct
    {
        [SerializeField] GameEventStruct<TStruct> EventObject;
        [DrawWithUnity]
        [SerializeField] UnityEvent<TStruct> Responce;
        // public abstract EventListenerStruct (GameEventStruct<TStruct> _evnet, UnityAction<TStruct> action);
        // {
        //     EventObject = _evnet;
        //     Responce = new UnityEvent<TStruct> ();
        //     Responce.AddListener (action);
        // }

        public void OnEventRaised (TStruct obj)
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
