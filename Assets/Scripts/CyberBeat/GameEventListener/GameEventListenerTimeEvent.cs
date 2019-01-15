
using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    
    public class GameEventListenerTimeEvent : MonoBehaviour
    {
        [SerializeField] GameEventTimeEvent Event;

        [SerializeField] UnityEventTimeEvent Responce;

        public void OnEventRaised (TimeEvent arg)
        {
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
        public class UnityEventTimeEvent : UnityEvent<TimeEvent>{}
    }
}

