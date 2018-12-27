using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "SkinsEnumDataSelector", menuName = "CyberBeat/EnumDataSelector/Skins")]
    public class SkinsEnumDataSelector : AEnumDataSelectorScriptableObject<SkinType, List<SkinItem>>
    {
        public List<SkinDataType> datas;
        public override List<TypeData<SkinType, List<SkinItem>>> Datas
        {
            get
            {
                return datas.Cast<TypeData<SkinType, List<SkinItem>>> ().ToList ();
            }
        }
        public override void Add (SkinType type, List<SkinItem> data)
        {
            datas.Add (new SkinDataType () { type = type, data = data });
        }
    }

    [Serializable]
    public class SkinDataType : TypeData<SkinType, List<SkinItem>> { }
}
