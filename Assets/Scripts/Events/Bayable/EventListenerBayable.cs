using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    [System.Serializable] public class EventListenerBayable : AEventListenerUnityObject<ABayable, EventListenerBayable>
    {
        [SerializeField] GameEventBayable _Event;
        public override AGameEventUnityObject<ABayable, EventListenerBayable> Event
        {
            get
            {
                return _Event;
            }
        }

        [SerializeField] UnityEventBayable responce;
        public override UnityEvent<ABayable> Responce { get { return responce; } }

        public EventListenerBayable (GameEventBayable _evnet, UnityAction<ABayable> action)
        {
            _Event = _evnet;
            responce = new UnityEventBayable ();
            Responce.AddListener (action);
        }
    }

}
