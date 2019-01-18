using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class BitGenerator : TransformObject
    {
        [SerializeField] KoreographerVariable koreographer;
        [SerializeField] LayerType TrackIDLayer;
        [SerializeField] UnityEventKoreographyEvent OnBit;
        [SerializeField] bool RegisterOnAwake;
        [SerializeField] bool RegisterOnStart;
        [SerializeField] bool debug;
        private void Awake()
        {
            if(RegisterOnAwake)
            {
                RegisterEvents();
                if(debug)
                {
                    Debug.Log("Register OnAwake");
                    printDebug();
                }
            }
        }
        private void Start() {
            if(RegisterOnStart)
            {
                RegisterEvents();
                if(debug)
                {
                    Debug.Log("Register OnStart");
                    printDebug();
                }
            }
        }
        public void RegisterEvents ()
        {
            Debug.LogFormat ("koreographer = {0}", koreographer);
            koreographer.RegisterForEvents (TrackIDLayer.ToString (), OnBit.Invoke);
        }

        void printDebug()
        {
            Debug.LogFormat(this, "{0} {1} {2}", koreographer, TrackIDLayer, name);
        }
    }
}
