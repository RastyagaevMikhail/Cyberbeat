using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Selectors/ColorVariable")]
    public class ColorVariableSelector : AEnumDataSelectorScriptableObject<string, ColorVariable>
    {
        public List<ColorVariableTypeData> datas;
        public override List<TypeData<string, ColorVariable>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, ColorVariable>> ().ToList ();
            }
        }

        [System.Serializable] public class ColorVariableTypeData : TypeData<string, ColorVariable> { }

        [SerializeField] string ValiadtePath = "Assets/Data/";
#if UNITY_EDITOR
        [ContextMenu ("Validate")]
        void Validate ()
        {
            ColorVariable[] colorVariable = Tools.GetAtPath<ColorVariable> (ValiadtePath);
            Debug.Log (colorVariable.Log ());
            datas = colorVariable
                .Select (p => new ColorVariableTypeData () { type = p.name, data = p }).ToList ();
            this.Save ();
        }

        public bool TryGetValue (string key, out Color color)
        {
            ColorVariable variable = null;
            
            bool result = Selector.TryGetValue (key, out variable);

            color = result ? variable : default (Color);

            return result;
        }
#endif
    }
}
