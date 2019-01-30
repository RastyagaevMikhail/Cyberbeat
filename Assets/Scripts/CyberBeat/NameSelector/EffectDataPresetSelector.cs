using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/EffectDataPreset")]
    public class EffectDataPresetSelector : AEnumDataSelectorScriptableObject<string, EffectDataPreset>
    {
        public List<EffectDataPresetTypeData> datas;
        public override List<TypeData<string, EffectDataPreset>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, EffectDataPreset>> ().ToList ();
            }
        }
        [System.Serializable] public class EffectDataPresetTypeData : TypeData<string, EffectDataPreset> { }
        [SerializeField] string ValiadtePath = "Assets/Data/";
    #if UNITY_EDITOR
        [ContextMenu ("Validate")]
        void Validate ()
        {
            datas = Tools.GetAtPath<EffectDataPreset> (ValiadtePath)
                .Select (p => new EffectDataPresetTypeData () { type = p.name, data = p }).ToList ();
            this.Save ();
        }
    #endif
    }
}