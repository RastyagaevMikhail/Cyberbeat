using GameCore;

using System;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{

    public class GameEventListenerIBitData : MonoBehaviour
    {
        [SerializeField] GameEventIBitData Event;

        [SerializeField] UnityEventIBitData Responce;
        [SerializeField] bool debug;

        public void OnEventRaised (IBitData arg)
        {
            if (debug)
                Debug.Log ($"{("OnEvent".a())} {$"[{Event.name}]".so()}\n{arg.ToString().red()}\n{("Raised On".a())} {name.mb()}", this);
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
