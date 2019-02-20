using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
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
        public Color[] GetColors ()
        {
            return items.Select (colorInfo => colorInfo.color).ToArray ();
        }
        Dictionary<Color, ColorInfo> _colors = new Dictionary<Color, ColorInfo> ();
        Dictionary<Color, ColorInfo> colors => _colors ?? (_colors = items.ToDictionary (ci => ci.color));
        public string GetName (Color color)
        {
            ColorInfo colorInfo =
                items.Find (
                    c => c.color.r == color.r &&
                    c.color.b == color.b &&
                    c.color.g == color.g
                );
            return colorInfo != null ? colorInfo.Name : String.Empty;
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
