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
            set =>  vertexColorMeshSetter.color = value;
            // {
            //     float opOld = Opacity;
            //     if (value.a != Opacity)
            //     {
            //         Opacity = opOld;
            //         vertexColorMeshSetter.color = value;
            //     }
            //     else
            //     {
            //         vertexColorMeshSetter.color = value;
            //     }
            // }
        }

        [SerializeField] VertexColorMeshSetter vertexColorMeshSetter;
        [SerializeField] Rotator rotator;
        [SerializeField] SpawnedObject spawnedObject;
        private void OnValidate ()
        {
            vertexColorMeshSetter = GetComponent<VertexColorMeshSetter> ();
            rotator = GetComponent<Rotator> ();
            spawnedObject = GetComponent<SpawnedObject> ();
        }
        protected override void Awake ()
        {
            base.Awake ();
            color = Color.white;
            Opacity = 0f;
        }
        EffectDataPreset preset;
        float FadeInTime => preset?preset.FadeInTime : 0f;
        float FadeOutTime => preset?preset.FadeOutTime : 0f;
        float LifeTime => preset?preset.LifeTime : 0f;
        public void InitSkin (EffectDataPreset preset)
        {
            // position = transform.TransformPoint (preset.OffsetPosition);
            this.preset = preset;

            FadeIn ();

            rotator.speed = preset.SpeedRotation;
        }
        private void FadeIn ()
        {
            if (FadeInTime != 0f)
            {
                FadeIn (FadeInTime, Life);
            }
            else
            {
                Opacity = 1f;
                Life ();
            }
        }

        private void Life ()
        {
            if (LifeTime != 0)
            {
                Invoke ("FadeOut", LifeTime);
            }
            else
            {
                FadeOut ();
            }
        }
        void FadeOut ()
        {
            if (FadeInTime != 0)
            {
                FadeOut (FadeInTime, PushToPool);
            }
            else
            {
                Opacity = 0;
                PushToPool();
            }
        }

        private void PushToPool ()
        {
            spawnedObject.PushToPool ();
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
