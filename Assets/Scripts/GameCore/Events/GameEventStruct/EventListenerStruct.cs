using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public abstract class EventListenerStruct<TStruct> where TStruct : struct
    {
        [SerializeField]
        protected abstract GameEventStruct<TStruct> AEventObject { get; }

        [SerializeField]
        protected abstract UnityEvent<TStruct> AResponce { get; }
        public virtual void OnEventRaised (TStruct obj)
        {
            AResponce.Invoke (obj);
        }
        public virtual bool OnEnable ()
        {
            // Debug.LogFormat (AEventObject, "EventObject.{1} = {0}", AEventObject, this);
            if (AEventObject)
                AEventObject.RegisterListener (this);
            return AEventObject;

        }

        public virtual bool OnDisable ()
        {
            if (AEventObject)
                AEventObject.UnRegisterListener (this);
            return AEventObject;
        }
    }
}
