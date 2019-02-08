using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    [CreateAssetMenu (
        fileName = "ColorInfoRuntimeSet.asset",
        menuName = "CyberBeat/RuntimeSet/ColorInfo")]
    public class ColorInfoRuntimeSet : RuntimeSet<ColorInfo>
    {
#if UNITY_EDITOR

        [Button] public void Validate ()
        {
            ForEach (colorInfo =>
                colorInfo.Count = Tools.ValidateVaraiable<IntVariable> ($"Assets/Resources/Data/Variables/Colors/{colorInfo.Name}.asset"));

            this.Save ();
        }
#endif
        public Color[] GetColors ()
        {
            return items.Select (colorInfo => colorInfo.color).ToArray ();
        }

        [SerializeField] UnityEventColorInfo onAddComplete;
        protected override UnityEvent<ColorInfo> OnAddComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }

        [SerializeField] UnityEventColorInfo onRemoveComplete;

        protected override UnityEvent<ColorInfo> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
    }
}
