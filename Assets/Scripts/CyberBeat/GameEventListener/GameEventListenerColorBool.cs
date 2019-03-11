using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{

    public class GameEventListenerColorBool : MonoBehaviour //ColorBool
    {
        [SerializeField] GameEventColorBool Event;

        [SerializeField] UnityEventColorBool Responce;

        [SerializeField] bool debug;

        public void OnEventRaised (Color arg1, bool arg2)
        {
            if (debug)
                Debug.Log ($"{("OnEvent".a())} {$"[{Event.name}]".so()}" +
                    $"\n{arg1.ToString().cyan()}" +
                    $"\n{arg2.ToString().cyan()}" +
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
