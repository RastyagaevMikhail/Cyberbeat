using System;
using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    
    public class GameEventListenerString : MonoBehaviour
    {
        [SerializeField] GameEventString Event;

        [SerializeField] UnityEventString Responce;
        
        [SerializeField] bool debug;

        public void OnEventRaised (String arg)
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
}

