using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ColorsStyle", menuName = "GameCore/ColorsStyle", order = 0)]
    public class ColorsStyle : Sirenix.OdinInspector.SerializedScriptableObject
    {
        public Dictionary<string, Color> Colors;
		public Sprite Icon;

        public void InitOnCreate()
        {
            Colors = new Dictionary<string, Color>();
            Colors.Add("DarkColor", Color.grey);
            Colors.Add("LightColor", Color.white);
        }
    }
}