using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class ComboData : SingletonData<ComboData>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/ComboData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate () { }
        public override void ResetDefault () { }
#endif

    }
}
