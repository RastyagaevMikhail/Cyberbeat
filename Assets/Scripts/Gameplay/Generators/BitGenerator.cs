﻿using GameCore;

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
        void Start ()
        {
            Debug.LogFormat ("koreographer = {0}", koreographer);
            koreographer.RegisterForEvents (TrackIDLayer.ToString (), OnBit.Invoke);
        }
    }
}
