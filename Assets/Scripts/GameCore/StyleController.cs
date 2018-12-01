using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    [Serializable]
    public class StyleController : SerializedMonoBehaviour //DevSkim: ignore DS184626 
    {
        public Dictionary<string, List<Graphic>> graphics;
        public ColorsStyle style;
        public void Awake ()
        {
            foreach (var graphicPair in graphics)
                foreach (var graphic in graphicPair.Value)
                    graphic.color = style.Colors[graphicPair.Key];
        }
        public void Validate (ColorsStyle style)
        {
            this.style = style;
            if (!style) return;
            if (graphics == null)
            {
                graphics = new Dictionary<string, List<Graphic>> ();
                foreach (var colorPair in style.Colors)
                    graphics.Add (colorPair.Key, new List<Graphic> ());
            }

            Awake ();

        }
    }
}
