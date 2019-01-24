using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/LayerTypeABitDataCollectionVariable")]
    public class LayerTypeABitDataCollectionVariableSelector : AEnumDataSelectorScriptableObject<LayerType, ABitDataCollectionVariable>
    {
        public List<LayerTypeABitDataCollectionVariableTypeData> datas;
        public override List<TypeData<LayerType, ABitDataCollectionVariable>> Datas
        {
            get
            {
                return datas.Cast<TypeData<LayerType, ABitDataCollectionVariable>> ().ToList ();
            }
        }
        [System.Serializable] public class LayerTypeABitDataCollectionVariableTypeData : TypeData<LayerType, ABitDataCollectionVariable> { }
    }
}