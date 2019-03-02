using GameCore.DoTween;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (menuName = "GameCore/Selectors/StringDoTweenGraphicsVariable")]
    public class StringDoTweenGraphicsVariableSelector : AEnumDataSelectorScriptableObject<String, DoTweenGraphicsVariable>
    {
        public List<StringDoTweenGraphicsVariableTypeData> datas;
        public override List<TypeData<String, DoTweenGraphicsVariable>> Datas
        {
            get
            {
                return datas.Cast<TypeData<String, DoTweenGraphicsVariable>> ().ToList ();
            }
        }

        [System.Serializable] public class StringDoTweenGraphicsVariableTypeData : TypeData<String, DoTweenGraphicsVariable> { }
    }
}
