using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    [Serializable]
    public class EventListenerVector2 : EventListenerStruct<Vector2>
    {
        [SerializeField] GameEventVector2 EventObject;
        protected override GameEventStruct<Vector2> AEventObject { get { return EventObject; } }

        [SerializeField] UnityEventVector2 Responce;
        protected override UnityEvent<Vector2> AResponce { get { return Responce; } }
        public EventListenerVector2 (GameEventVector2 _evnet, UnityAction<Vector2> action)
        {
            EventObject = _evnet;
            Responce = new UnityEventVector2 ();
            Responce.AddListener (action);
        }

        public override void OnEventRaised (Vector2 obj)
        {
            Responce.Invoke (obj);
        }
    }
}
