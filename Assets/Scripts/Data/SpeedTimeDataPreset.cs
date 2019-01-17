using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/MetaData/SpeedTime")]
    public class SpeedTimeDataPreset : ScriptableObject, IMetaDataPreset<SpeedTimeData>, IMetaDataPreset
    {
        [SerializeField] SpeedTimeData data;
        object IMetaDataPreset.Data { get { return data; } }
        public SpeedTimeData Data { get { return data; } }
    }
}
