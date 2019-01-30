using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class EffectDataPreset : ScriptableObject
    {
        [SerializeField] EffectSkinData skinData;
        [SerializeField] Shapes shape;
        [SerializeField] float speedRotation;
        [SerializeField] float fadeInTime;

        public EffectSkinData SkinData { get => skinData; }
        public Shapes Shape { get => shape; }
        public float SpeedRotation { get => speedRotation; }
        public float FadeInTime { get => fadeInTime; }

        public void Init (EffectSkinData skinData, Shapes shape, float speedRotation, float fadeInTime)
        {
            this.skinData = skinData;
            this.shape = shape;
            this.speedRotation = speedRotation;
            this.fadeInTime = fadeInTime;
        }
    }
}
