using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class BoosterDataUnityEventSelector : AEnumDataSelectorMonoBehaviour<BoosterData, UnityEvent>
    {
        [SerializeField] List<BoosterDataUnityEventTypeData> datas;
        public override List<TypeData<BoosterData, UnityEvent>> Datas
        {
            get
            {
                return datas.Cast<TypeData<BoosterData, UnityEvent>> ().ToList ();
            }
        }

        [System.Serializable]
        public class BoosterDataUnityEventTypeData : TypeData<BoosterData, UnityEvent>
        {

        }

        public void Select (BoosterData boosterData)
        {
            this [boosterData].Invoke ();
        }
    }
}
