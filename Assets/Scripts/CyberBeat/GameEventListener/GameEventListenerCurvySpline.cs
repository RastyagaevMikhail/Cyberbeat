using FluffyUnderware.Curvy;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{

    public class GameEventListenerCurvySpline : MonoBehaviour
    {
        [SerializeField] GameEventCurvySpline Event;
        [SerializeField] UnityEventCurvySpline Responce;

        public void OnEventRaised (CurvySpline arg)
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
        public class UnityEventCurvySpline : UnityEvent<CurvySpline> { }
    }
}
