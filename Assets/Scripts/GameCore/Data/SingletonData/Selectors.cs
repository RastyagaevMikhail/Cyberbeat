using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;

using UnityEngine;

namespace GameCore
{
    public class Selectors : SingletonData<Selectors>
    {
#if UNITY_EDITOR
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Selectors")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
#endif
        public override void InitOnCreate ()
        {
            selectors = Tools.GetAtPath<ASelectorScriptableObject> ("Assets").ToList ();
        }
        public override void ResetDefault () { }
#endif

        [SerializeField] List<ASelectorScriptableObject> selectors;
    }
}
