using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class SkinItemTunnelBuilderSelector : AEnumDataSelectorMonoBehaviour<SkinItem, GameObject>
    {
        [SerializeField] List<SkinItemTunnelBuilderTypeData> datas;
        public override List<TypeData<SkinItem, GameObject>> Datas
        {
            get
            {
                return datas.Cast<TypeData<SkinItem, GameObject>> ().ToList ();
            }
        }
    }

    [Serializable]
    public class SkinItemTunnelBuilderTypeData : TypeData<SkinItem, GameObject>
    {

    }
}
