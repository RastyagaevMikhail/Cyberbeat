using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{

    public class GameEventListenerInputControlType : MonoBehaviour
    {
        [SerializeField] GameEventInputControlType Event;

        [SerializeField] UnityEventInputControlType Responce;
        [SerializeField] bool debug;

        public void OnEventRaised (InputControlType arg)
        {
            Responce.Invoke (arg);
            if (debug) Debug.Log (Responce.Log (arg), this);
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
        public class UnityEventInputControlType : UnityEvent<InputControlType> { }
    }
}
