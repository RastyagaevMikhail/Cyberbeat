using GameCore;

using Sirenix.OdinInspector;

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

        [Button]
        public void Validate ()
        {
            datas = new List<LayerTypeABitDataCollectionVariableTypeData>();
            foreach (var layer in Enums.instance.LayerTypes)
            {
                datas.Add(new LayerTypeABitDataCollectionVariableTypeData(){
                    type = layer,
                    data = Tools.ValidateSO<ABitDataCollectionVariable>($"Assets/Data/Variables/ABitDataCollection/Current{layer.name}Collection.asset")
                });
            }
        }
    }
}
