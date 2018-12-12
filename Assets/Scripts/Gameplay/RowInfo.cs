using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using GameCore;

using Sirenix.OdinInspector;
namespace CyberBeat
{
    [System.Serializable]
    public class RowInfo
    {

        public SpawnedObject this [int index]
        {
            get { if (index == 0) return Object1; if (index == 1) return Object2; if (index == 2) return Object3; if (index == 3) return Object4; if (index == 4) return Object5; return null; }
            set { if (index == 0) Object1 = value; if (index == 1) Object2 = value; if (index == 2) Object3 = value; if (index == 3) Object4 = value; if (index == 4) Object5 = value; }
        }

        [HorizontalGroup, PreviewField, HideLabel]
        [SerializeField] /* GameObject */ SpawnedObject Object1 = null;
        [HorizontalGroup, PreviewField, HideLabel]
        [SerializeField] /* GameObject */ SpawnedObject Object2 = null;
        [HorizontalGroup, PreviewField, HideLabel]
        [SerializeField] /* GameObject */ SpawnedObject Object3 = null;
        [HorizontalGroup, PreviewField, HideLabel]
        [SerializeField] /* GameObject */ SpawnedObject Object4 = null;
        [HorizontalGroup, PreviewField, HideLabel]
        [SerializeField] /* GameObject */ SpawnedObject Object5 = null;

        public RowInfo () { }
        public RowInfo (RowInfo other)
        {
            this [0] = other[0];
            this [1] = other[1];
            this [2] = other[2];
            this [3] = other[3];
            this [4] = other[4];
        }
    }
}
