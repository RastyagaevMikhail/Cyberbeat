using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/SpeedTimeDataPreset")]
    public class SpeedTimeDataPresetSelector : AEnumDataSelectorScriptableObject<string, SpeedTimeDataPreset>
    {
        public List<SpeedTimeDataPresetTypeData> datas;
        public override List<TypeData<string, SpeedTimeDataPreset>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, SpeedTimeDataPreset>> ().ToList ();
            }
        }

        [System.Serializable] public class SpeedTimeDataPresetTypeData : TypeData<string, SpeedTimeDataPreset> { }
    }
}
