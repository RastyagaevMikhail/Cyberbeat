using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Data/Selectors")]
    public class Selectors : ScriptableObject
    {
#if UNITY_EDITOR

        [Button (ButtonSizes.Large)]
        [ContextMenu ("Validate Selectors")]
        public void Validate ()
        {
            selectors = Tools.GetAtPath<ASelectorScriptableObject> ("Assets").ToList ();
        }
#endif
        public void Init ()
        {
            selectors.ForEach (s => s.OnEnable ());
        }

        [ListDrawerSettings (
            Expanded = true,
            IsReadOnly = true,
            HideAddButton = true,
            HideRemoveButton = true,
            ShowPaging = false)]
        [PropertyOrder (int.MaxValue)]
        [SerializeField]
        List<ASelectorScriptableObject> selectors;
    }
}
