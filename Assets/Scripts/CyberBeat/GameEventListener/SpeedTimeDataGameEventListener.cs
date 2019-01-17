
using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    
    public class SpeedTimeDataGameEventListener : MonoBehaviour
    {
        [SerializeField] SpeedTimeDataGameEvent Event;

        [SerializeField] UnityEventSpeedTimeData Responce;

        public void OnEventRaised (SpeedTimeData arg)
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
        
    }
}

