using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class EffectDataPreset : ScriptableObject
    {
        [SerializeField] string prefabName;
        [SerializeField] float speedRotation;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float lifeTime = 1f;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] Vector3 offsetPosition;

        public float SpeedRotation { get => speedRotation; }
        public float FadeInTime { get => fadeInTime; }
        public Vector3 OffsetPosition { get => offsetPosition; }
        public string PrefabName { get => prefabName; }
        public float LifeTime { get => lifeTime; }
        public float FadeOutTime { get => fadeOutTime; }

        public void Init (string prefabName)
        {
            this.prefabName = prefabName;
        }

        [Button] public void SaveMe () { this.Save (); }
        public override string ToString ()
        {
            return base.ToString () + "\n" +
                $"prefabName:{prefabName}\n" +
                $"speedRotation:{speedRotation}\n" +
                $"fadeInTime:{fadeInTime}\n" +
                $"lifeTime:{lifeTime}\n" +
                $"fadeOutTime:{fadeOutTime}\n" +
                $"offsetPosition:{offsetPosition}\n";
        }
    }
}
