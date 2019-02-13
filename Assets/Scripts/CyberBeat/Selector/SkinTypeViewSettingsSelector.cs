using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/SkinTypeViewSettings")]
    public class SkinTypeViewSettingsSelector : AEnumDataSelectorScriptableObject<SkinType, ViewSettings>
    {
        public List<SkinTypeViewSettingsTypeData> datas;
        public override List<TypeData<SkinType, ViewSettings>> Datas
        {
            get
            {
                return datas.Cast<TypeData<SkinType, ViewSettings>> ().ToList ();
            }
        }
        [System.Serializable] public class SkinTypeViewSettingsTypeData : TypeData<SkinType, ViewSettings> { }
    }
}