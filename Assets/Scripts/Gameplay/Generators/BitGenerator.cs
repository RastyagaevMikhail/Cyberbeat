using GameCore;
using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class BitGenerator : TransformObject
    {
        [SerializeField] LayerType TrackIDLayer;
        [SerializeField]UnityEventKoreographyEvent OnBit;
        void Start ()
        {
            Koreographer.Instance.RegisterForEvents(TrackIDLayer.ToString(),OnBit.Invoke);
        }
    }
}
