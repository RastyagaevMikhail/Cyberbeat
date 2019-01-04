using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerVector3 : EventListenerStruct<Vector3>
    {
        [SerializeField] GameEventVector3 EventObject;
        protected override GameEventStruct<Vector3> AEventObject { get { return EventObject; } }

        [SerializeField] UnityEventVector3 Responce;
        protected override UnityEvent<Vector3> AResponce { get { return Responce; } }
        public EventListenerVector3 (GameEventVector3 _evnet, UnityAction<Vector3> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventVector3 ();
            Responce.AddListener (action);
        }
        public override void OnEventRaised (Vector3 obj)
        {
            Responce.Invoke (obj);
        }
    }
}
