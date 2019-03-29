using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/LayerTypeABitDataCollectionVariable")]
    public class LayerTypeABitDataCollectionVariableSelector : AEnumDataSelectorScriptableObject<LayerType, TrackBitsCollectionVariable>
    {
        [ListDrawerSettings (
            Expanded = true,
            IsReadOnly = true,
            HideAddButton = true,
            HideRemoveButton = true,
            ShowPaging = false)]
        public List<LayerTypeABitDataCollectionVariableTypeData> datas;
        public override List<TypeData<LayerType, TrackBitsCollectionVariable>> Datas
        {
            get
            {
                return datas.Cast<TypeData<LayerType, TrackBitsCollectionVariable>> ().ToList ();
            }
        }

        [System.Serializable] public class LayerTypeABitDataCollectionVariableTypeData : TypeData<LayerType, TrackBitsCollectionVariable> { }
#if UNITY_EDITOR
        [ContextMenu("Validate")]
        void Validate()
        {
            Validate(Tools.GetAssetAtPath<Enums>("Assets/Resources/Data/Enums.asset"));
        }

        public void Validate (Enums enums )
        {
            datas = new List<LayerTypeABitDataCollectionVariableTypeData> ();
            foreach (LayerType layer in enums.GetValues<LayerType> ())
            {
                datas.Add (new LayerTypeABitDataCollectionVariableTypeData ()
                {
                type = layer,
                data = Tools.ValidateSO<TrackBitsCollectionVariable> ($"Assets/Data/Variables/ABitDataCollection/Current{layer.name}Collection.asset")
                });
            }
            this.Save();
        }
#endif    
    }
}
