using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;

using UnityEngine;

namespace GameCore
{
    public class Selectors : SingletonData<Selectors>, IStartInitializationData
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Selectors")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

        [ContextMenu ("Validate Selectors")]
        public override void InitOnCreate ()
        {
            selectors = Tools.GetAtPath<ASelectorScriptableObject> ("Assets").ToList ();
        }
        public override void ResetDefault () { }
#else
        public override void ResetDefault () { }
#endif
        public void Init (bool consentValue)
        {
            selectors.ForEach (s => s.OnEnable ());
        }

        [SerializeField] List<ASelectorScriptableObject> selectors;
    }
}
