
using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    
    public class GameEventListenerAdTypeString : MonoBehaviour //AdTypeString
    {
        [SerializeField] GameEventAdTypeString Event;

        [SerializeField] UnityEventAdTypeString Responce;
        
        [SerializeField] bool debug;

        public void OnEventRaised (AdType arg1, String arg2)
        {
             if (debug)
                Debug.Log ($"{("OnEvent".a())} {$"[{Event.name}]".so()}"+
                $"\n{arg1.ToString().cyan()}"+
                $"\n{arg2.ToString().cyan()}"+
                $"\n{("Raised On".a())} {name.mb()}", this);
            Responce.Invoke (arg1, arg2);
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

