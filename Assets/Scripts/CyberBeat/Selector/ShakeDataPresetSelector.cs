using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/stringShakeDataPreset")]
    public class ShakeDataPresetSelector : AEnumDataSelectorScriptableObject<string, ShakeDataPreset>
    {
        
        public List<ShakeDataPresetTypeData> datas;
        public override List<TypeData<string, ShakeDataPreset>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, ShakeDataPreset>> ().ToList ();
            }
        }

        [System.Serializable] public class ShakeDataPresetTypeData : TypeData<string, ShakeDataPreset> { }
        [SerializeField] string ValiadtePath = "Assets/Data/MetaData/Shake";
#if UNITY_EDITOR

        [ContextMenu ("Validate")]
        void Validate ()
        {
            datas = Tools.GetAtPath<ShakeDataPreset> (ValiadtePath)
                .Select (p => new ShakeDataPresetTypeData () { type = p.name, data = p }).ToList ();
            this.Save ();
        }
#endif
    }
}
