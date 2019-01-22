using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace FluffyUnderware.Curvy
{
    
    public class GameEventListenerCurvySplineSegment : MonoBehaviour
    {
        [SerializeField] GameEventCurvySplineSegment Event;

        [SerializeField] UnityEventCurvySplineSegment Responce;
        
        [SerializeField] bool debug;

        public void OnEventRaised (CurvySplineSegment arg)
        {
             if (debug)
                Debug.Log ($"{("OnEvent".a())} {$"[{Event.name}]".so()}\n{arg.ToString().cyan()}\n{("Raised On".a())} {name.mb()}", this);
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
    [Serializable] public class UnityEventCurvySplineSegment : UnityEvent<CurvySplineSegment>{}
}

