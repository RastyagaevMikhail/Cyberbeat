using GameCore;

using System;
using System.Collections;

using Timers;

using UnityEngine;
namespace CyberBeat
{
    public class EffectSkinSetter : TransformObject
    {
        public float Opacity { get => vertexColorMeshSetter.alpha; set => vertexColorMeshSetter.alpha = value; }
        public Color color
        {
            get => vertexColorMeshSetter.color;
            set
            {
                float opOld = Opacity;
                vertexColorMeshSetter.color = value;
                if (value.a != Opacity)
                    Opacity = opOld;
            }
        }

        [SerializeField] VertexColorMeshSetter vertexColorMeshSetter;
        [SerializeField] Rotator rotator;
        [SerializeField] SpawnedObject spawnedObject;
        private void OnValidate ()
        {
            spawnedObject = GetComponent<SpawnedObject> ();
        }
        protected override void Awake ()
        {
            base.Awake ();
            color = Color.white;
            Opacity = 0f;
        }

        public void InitSkin (EffectDataPreset preset)
        {
            transform.position = transform.TransformPoint (preset.OffsetPosition);

            FadeIn (preset.FadeInTime);

            if (preset.LifeTime != 0)
                TimersManager.SetTimer (this, preset.FadeInTime + preset.LifeTime, 0, () => FadeOut (preset.FadeOutTime));
            else if (preset.FadeOutTime != 0)
                FadeOut (preset.FadeOutTime, () => spawnedObject.PushToPool ());
            else
                spawnedObject.PushToPool ();

            rotator.speed = preset.SpeedRotation;
        }

        public void FadeIn (float duration = 1f, Action onComplete = null) => DOVirtualFloat (0f, 1f, duration, value => Opacity = value, onComplete);
        public void FadeOut (float duration = 1f, Action onComplete = null) => DOVirtualFloat (1f, 0f, duration, value => Opacity = value, onComplete);
        void DOVirtualFloat (float startValue, float endValue, float duration, Action<float> action, Action onComplete = null) =>
            StartCoroutine (cr_DOVirtualFloat (startValue, endValue, duration, action, onComplete));
        IEnumerator cr_DOVirtualFloat (float startValue, float endValue, float duration, Action<float> action, Action onComplete = null)
        {
            float t = 0;
            while (t <= duration)
            {
                t += Time.deltaTime / duration;
                action (Mathf.Lerp (startValue, endValue, t));
                yield return null;
            }
            action (endValue);
            if (onComplete != null)
                onComplete ();
        }
    }
}
