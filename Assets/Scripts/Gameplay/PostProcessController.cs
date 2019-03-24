using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CyberBeat
{
    public class PostProcessController : MonoBehaviour
    {
        [SerializeField] PostProcessProfile currentProfile;
        [SerializeField] ColorInfoRuntimeSetVariable CurrentColorSet;
        [SerializeField] ColorSelector colorSelector;
        private Bloom _bloom = null;
        public Bloom bloom { get { if (_bloom == null) _bloom = currentProfile.GetSetting<Bloom> (); return _bloom; } }
        public void OnColorChanged (Color color)
        {
            string colorName = CurrentColorSet.Value.GetName (color);

            ColorBloom = colorSelector[colorName];
        }

        public Color ColorBloom { set => bloom.color.value = value; }
    }
}
