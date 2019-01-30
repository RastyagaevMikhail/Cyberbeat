using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/EffectSkinData")]
    public class EffectSkinDataSelector : AEnumDataSelectorScriptableObject<string, EffectSkinData>
    {
        public List<EffectSkinDataTypeData> datas;
        public override List<TypeData<string, EffectSkinData>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, EffectSkinData>> ().ToList ();
            }
        }
        [System.Serializable] public class EffectSkinDataTypeData : TypeData<string, EffectSkinData> { }
        [SerializeField] string ValiadtePath = "Assets/Data/";
    #if UNITY_EDITOR
        [ContextMenu ("Validate")]
        void Validate ()
        {
            datas = Tools.GetAtPath<EffectSkinData> (ValiadtePath)
                .Select (p => new EffectSkinDataTypeData () { type = p.name, data = p }).ToList ();
            this.Save ();
        }
    #endif
    }
}