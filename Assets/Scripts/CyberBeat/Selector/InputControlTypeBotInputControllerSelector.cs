
using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/InputControlTypeBotInputController")]
    public class InputControlTypeBotInputControllerSelector : AEnumDataSelectorScriptableObject<InputControlType, BotInputController>
    {
        public List<InputControlTypeBotInputControllerTypeData> datas;
        public override List<TypeData<InputControlType, BotInputController>> Datas
        {
            get
            {
                return datas.Cast<TypeData<InputControlType, BotInputController>> ().ToList ();
            }
        }
        [System.Serializable] public class InputControlTypeBotInputControllerTypeData : TypeData<InputControlType, BotInputController> { }
    }
}