using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{

    public class GameEventListenerTimeEvent : MonoBehaviour
    {
        [SerializeField] GameEventTimeEvent Event;

        [SerializeField] UnityEventTimeEvent Responce;
        [SerializeField] bool enableFilter;
        [SerializeField] string payloadFilter;
        public void OnEventRaised (TimeEvent arg)
        {
            if (enableFilter && arg.timeOfEvent.payload == payloadFilter)
                Responce.Invoke (arg);
            else
                Responce.Invoke (arg);
        }
        public void OnEnable ()
        {
            Event.RegisterListener (this);
        }

        public void OnDisable ()
        {
            Event.UnRegisterListener (this);
        }

        [Serializable]
        public class UnityEventTimeEvent : UnityEvent<TimeEvent> { }
    }
}
