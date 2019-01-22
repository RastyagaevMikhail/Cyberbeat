using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/ChaseCamDataPreset")]
    public class ChaseCamDataPresetSelector : AEnumDataSelectorScriptableObject<string, ChaseCamDataPreset>
    {
        public List<ChaseCamDataPresetTypeData> datas;
        public override List<TypeData<string, ChaseCamDataPreset>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, ChaseCamDataPreset>> ().ToList ();
            }
        }

        [System.Serializable] public class ChaseCamDataPresetTypeData : TypeData<string, ChaseCamDataPreset> { }
    }
}
