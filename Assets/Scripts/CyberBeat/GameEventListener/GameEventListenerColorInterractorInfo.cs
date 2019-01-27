using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    
    public class GameEventListenerColorInterractorInfo : MonoBehaviour
    {
        [SerializeField] GameEventColorInterractorInfo Event;

        [SerializeField] UnityEventColorInterractorInfo Responce;
        
        [SerializeField] bool debug;

        public void OnEventRaised (ColorInterractor.Info arg)
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
    [Serializable] public class UnityEventColorInterractorInfo : UnityEvent<ColorInterractor.Info>{}
}

