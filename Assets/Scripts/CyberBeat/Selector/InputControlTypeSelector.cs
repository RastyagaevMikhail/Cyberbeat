using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/InputControlType")]
    public class InputControlTypeSelector : AEnumDataSelectorScriptableObject<string, InputControlType>
    {
        public List<InputControlTypeTypeData> datas;
        public override List<TypeData<string, InputControlType>> Datas
        {
            get
            {
                return datas.Cast<TypeData<string, InputControlType>> ().ToList ();
            }
        }

        [System.Serializable] public class InputControlTypeTypeData : TypeData<string, InputControlType> { }

        [SerializeField] string ValiadtePath = "Assets/Data/";
#if UNITY_EDITOR
        [ContextMenu ("Validate")]
        void Validate ()
        {
            datas = Tools.GetAtPath<InputControlType> (ValiadtePath)
                .Select (p => new InputControlTypeTypeData () { type = p.name, data = p }).ToList ();
            this.Save ();
        }
#endif
        [SerializeField] InputControlTypeVariable variable;
        public void SetVariable (string name)
        {
            variable.Value = Selector[name];
        }
    }
}
