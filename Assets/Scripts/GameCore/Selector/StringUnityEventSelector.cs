using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public class StringUnityEventSelector : AEnumDataSelectorMonoBehaviour<String, UnityEvent>
    {
        public List<StringUnityEventTypeData> datas;
        public override List<TypeData<String, UnityEvent>> Datas
        {
            get
            {
                return datas.Cast<TypeData<String, UnityEvent>> ().ToList ();
            }
        }

        [System.Serializable] public class StringUnityEventTypeData : TypeData<String, UnityEvent> { }

        public void Invoke (string key)
        {
            Selector[key].Invoke ();
        }
    }
}
