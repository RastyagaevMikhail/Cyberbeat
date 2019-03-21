
using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace UnityEngine.Monetization
{
    [CreateAssetMenu (menuName = "UnityEngine.Monetization/Selectors/ShowResultUnityEventString")]
    public class ShowResultUnityEventStringSelector : AEnumDataSelectorScriptableObject<ShowResult, UnityEventString>
    {
        public List<ShowResultUnityEventStringTypeData> datas;
        public override List<TypeData<ShowResult, UnityEventString>> Datas
        {
            get
            {
                return datas.Cast<TypeData<ShowResult, UnityEventString>> ().ToList ();
            }
        }
        [System.Serializable] public class ShowResultUnityEventStringTypeData : TypeData<ShowResult, UnityEventString> { }
    }
}