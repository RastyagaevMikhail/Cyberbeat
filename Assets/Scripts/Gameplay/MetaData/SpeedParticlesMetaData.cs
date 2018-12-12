using GameCore;

using FluffyUnderware.Curvy;

using Sirenix.OdinInspector;
using Sirenix.Utilities;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SpeedParticlesMetaData : MonoBehaviour, ICurvyMetadata, IMetaData
    {
        [HideLabel]
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
