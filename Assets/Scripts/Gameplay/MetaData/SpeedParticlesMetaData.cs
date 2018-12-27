using FluffyUnderware.Curvy;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SpeedParticlesMetaData : MonoBehaviour, ICurvyMetadata, IMetaData
    {
        public PartsData data;
        public string NameOfMetaType
        {
            get
            {
                return this.GetType ().Name.Replace ("MetaData", "").SplitPascalCase ();
            }
        }
    }
}
