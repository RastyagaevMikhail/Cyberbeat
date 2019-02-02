using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;
#endif
namespace CyberBeat {
    [System.Serializable]
    public class Preset {
        public string Id;
        public List<Material> Objects;
    }
}