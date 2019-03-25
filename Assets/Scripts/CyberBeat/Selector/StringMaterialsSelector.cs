using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/StringMaterials")]
    public class StringMaterialsSelector : AEnumDataSelectorScriptableObject<String, List<Material>>
    {
        public List<StringMaterialsTypeData> datas;
        public override List<TypeData<String, List<Material>>> Datas
        {
            get
            {
                return datas.Cast<TypeData<String, List<Material>>> ().ToList ();
            }
        }

        [System.Serializable] public class StringMaterialsTypeData : TypeData<String, List<Material>> { }
    }
}
