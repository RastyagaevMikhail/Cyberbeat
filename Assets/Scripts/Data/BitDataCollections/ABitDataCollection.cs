using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;
namespace CyberBeat
{
    public abstract class ABitDataCollection : ScriptableObject 
    {
        public abstract List<IBitData> Bits { get; }
        public abstract void Init(List<KoreographyEvent> events);
    }
}
