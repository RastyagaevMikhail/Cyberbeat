using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/InputControlTypeAInputController")]
    public class InputControlTypeAInputControllerSelector : AEnumDataSelectorScriptableObject<InputControlType, AInputController>
    {
        public List<InputControlTypeAInputControllerTypeData> datas;
        public override List<TypeData<InputControlType, AInputController>> Datas
        {
            get
            {
                return datas.Cast<TypeData<InputControlType, AInputController>> ().ToList ();
            }
        }
        [System.Serializable] public class InputControlTypeAInputControllerTypeData : TypeData<InputControlType, AInputController> { }
    }
}