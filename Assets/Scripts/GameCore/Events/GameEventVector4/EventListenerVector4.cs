using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerVector4 : EventListenerStruct<Vector4>
    {
        [SerializeField] GameEventVector4 EventObject;
        protected override GameEventStruct<Vector4> AEventObject { get { return EventObject; } }

        [SerializeField] UnityEventVector4 Responce;
        protected override UnityEvent<Vector4> AResponce { get { return Responce; } }
        public EventListenerVector4 (GameEventVector4 _evnet, UnityAction<Vector4> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventVector4 ();
            Responce.AddListener (action);
        }

        public override void OnEventRaised (Vector4 obj)
        {
            Responce.Invoke (obj);
        }
    }
}
