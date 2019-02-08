using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameCore
{
    
    public class GameEventListenerGraphic : MonoBehaviour
    {
        [SerializeField] GameEventGraphic Event;

        [SerializeField] UnityEventGraphic Responce;
        
        [SerializeField] bool debug;

        public void OnEventRaised (Graphic arg)
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
    [Serializable] public class UnityEventGraphic : UnityEvent<Graphic>{}
}

