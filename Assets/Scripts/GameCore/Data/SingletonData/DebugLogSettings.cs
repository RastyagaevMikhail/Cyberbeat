using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{

    public class DebugLogSettings : SingletonData<DebugLogSettings>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Debug Log Settings")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void InitOnCreate () { }
        public override void ResetDefault () { }
#endif

        public Color MonoBehaviourColor = new Color (31,36,119,255)/255f;
        public Color ScriptableObjectColor = new Color (54, 53, 55,255)/255f;
        public Color ActionColor = new Color (0,102,51,255)/255f;
        public Color ErrorColor = new Color (179,0,0,255)/255f;
        public Color WarningColor = new Color (248,201,20,255)/255f;

    }
}
