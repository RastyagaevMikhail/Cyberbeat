using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Selectors/StringString")]
    public class StringStringSelector : AEnumDataSelectorScriptableObject<String, String>
    {
        public List<StringStringTypeData> datas;
        public override List<TypeData<String, String>> Datas
        {
            get
            {
                return datas.Cast<TypeData<String, String>> ().ToList ();
            }
        }

        [System.Serializable] public class StringStringTypeData : TypeData<String, String> { }
    }
}
