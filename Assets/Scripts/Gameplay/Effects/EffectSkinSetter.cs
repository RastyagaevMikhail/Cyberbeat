using GameCore;

using System;
using System.Collections;

using UnityEngine;

namespace CyberBeat
{
    public class EffectSkinSetter : TransformObject
    {     
        public float Opacity { get => vertexColorMeshSetter.alpha; set => vertexColorMeshSetter.alpha = value; }
        public Color color { get => vertexColorMeshSetter.color; set => vertexColorMeshSetter.color = value; }
        [SerializeField] VertexColorMeshSetter vertexColorMeshSetter;
        [SerializeField] Rotator rotator;
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
            rotator.speed = preset.SpeedRotation;
        }

        public void FadeIn (float duration = 1f) => DOVirtualFloat (0f, 1f, duration, value => Opacity = value);
        public void FadeOut (float duration = 1f) => DOVirtualFloat (1f, 0f, duration, value => Opacity = value);
        void DOVirtualFloat (float startValue, float endValue, float duration, Action<float> action) =>
            StartCoroutine (cr_DOVirtualFloat (startValue, endValue, duration, action));
        IEnumerator cr_DOVirtualFloat (float startValue, float endValue, float duration, Action<float> action)
        {
            float t = 0;
            while (t <= duration)
            {
                t += Time.deltaTime / duration;
                action (Mathf.Lerp (startValue, endValue, t));
                yield return null;
            }
            action (endValue);
        }
    }
}
