using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    [CreateAssetMenu (fileName = "SkinIndexSelector", menuName = "CyberBeat/EnumDataSelector/SkinIndex")]
    public class SkinIndexSelector : AEnumDataSelectorScriptableObject<SkinType, IntVariable>
    {
        public List<SkinIndexTypeData> datas;
        public override List<TypeData<SkinType, IntVariable>> Datas
        {
            get
            {
                return datas.Cast<TypeData<SkinType, IntVariable>> ().ToList ();
            }
        }
    }

    [Serializable]
    public class SkinIndexTypeData : TypeData<SkinType, IntVariable> { }
}
