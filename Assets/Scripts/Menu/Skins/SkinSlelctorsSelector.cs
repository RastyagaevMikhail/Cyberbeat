using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class SkinSlelctorsSelector : TransformObjectSelector<SkinType>
    {
        [SerializeField] List<SkinData> datas;
        public override List<TypeData<SkinType, TransformObject>> Datas
        {
            get
            {
                return datas.Cast<TypeData<SkinType, TransformObject>> ().ToList ();
            }
        }
    }

    [Serializable]
    public class SkinData : TypeData<SkinType, TransformObject> { }
}
