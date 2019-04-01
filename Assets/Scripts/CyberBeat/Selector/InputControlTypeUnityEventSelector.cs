using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    public class InputControlTypeUnityEventSelector : AEnumDataSelectorMonoBehaviour<InputControlType, UnityEvent>
    {
        public List<InputControlTypeUnityEventTypeData> datas;
        public override List<TypeData<InputControlType, UnityEvent>> Datas
        {
            get
            {
                return datas.Cast<TypeData<InputControlType, UnityEvent>> ().ToList ();
            }
        }

        [System.Serializable] public class InputControlTypeUnityEventTypeData : TypeData<InputControlType, UnityEvent> { }
        public void Invoke (InputControlType inputControlType)
        {
            UnityEvent unityEvent;
            Selector.TryGetValue (inputControlType, out unityEvent);

            unityEvent?.Invoke ();
        }
    }
}
