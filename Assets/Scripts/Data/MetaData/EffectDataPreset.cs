using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class EffectDataPreset : ScriptableObject
    {
        [SerializeField] string prefabName;
        [SerializeField] float speedRotation;
        [SerializeField] float fadeInTime;
        [SerializeField] Vector3 offsetPosition;

        public float SpeedRotation { get => speedRotation; }
        public float FadeInTime { get => fadeInTime; }
        public Vector3 OffsetPosition { get => offsetPosition; }
        public string PrefabName { get => prefabName; }

        public void Init (string prefabName)
        {
            this.prefabName = prefabName;
        }
    }
}
