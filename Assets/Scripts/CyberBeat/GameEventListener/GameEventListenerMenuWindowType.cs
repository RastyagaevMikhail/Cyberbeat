using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    
    public class GameEventListenerMenuWindowType : MonoBehaviour
    {
        [SerializeField] GameEventMenuWindowType Event;

        [SerializeField] UnityEventMenuWindowType Responce;
        
        [SerializeField] bool debug;

        public void OnEventRaised (MenuWindowType arg)
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
    [Serializable] public class UnityEventMenuWindowType : UnityEvent<MenuWindowType>{}
}

