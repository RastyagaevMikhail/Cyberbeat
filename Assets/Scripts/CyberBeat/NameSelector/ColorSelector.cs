using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/Color")]
    public class ColorSelector : ScriptableObject
    {
        public List<ColorTypeData> datas;

        [System.Serializable] public class ColorTypeData : EmptyData
        {
            public string name;
            [ColorUsage (true, true)] public Color color;
        }
        public Color this [string name] => selector[name];
        Dictionary<string, Color> _selector = null;
        Dictionary<string, Color> selector => _selector??(_selector = datas.ToDictionary (d => d.name, d => d.color));
    }
}
