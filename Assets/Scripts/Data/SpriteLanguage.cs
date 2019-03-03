using System;

using UnityEngine;

namespace CyberBeat
{
    [Serializable]
    public class SpriteLanguage
    {
        [SerializeField] SystemLanguage lang;
        [SerializeField] Sprite sprite;

        public SystemLanguage Lang => lang;
        public Sprite Sprite => sprite;
    }
}
