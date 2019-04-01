using GameCore;

using System;
using System.Collections;

using Timers;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (MeshFilter), typeof (MeshRenderer))]
    [RequireComponent (typeof (Rotator), typeof (SpawnedObject))]
    [RequireComponent (typeof (BoxCollider), typeof (TriggerTagActions))]
    [RequireComponent (typeof (GameEventListenerColor))]
    [ExecuteInEditMode]
    public class EffectSkinSetter : TransformObject
    {
        MaterialPropertyBlock prop;
        [SerializeField, HideInInspector] int OpacityIDProp;

        [SerializeField, HideInInspector] int ColorIDProp;

        public float Opacity
        {
            get
            {
                mRend.GetPropertyBlock (prop);
                return prop.GetFloat (OpacityIDProp);
            }
            set
            {
                mRend.GetPropertyBlock (prop);
                prop.SetFloat (OpacityIDProp, value);
                mRend.SetPropertyBlock (prop);
            }
        }
        public Color color
        {
            get
            {
                mRend.GetPropertyBlock (prop);
                return prop.GetColor (ColorIDProp);
            }
            set
            {
                mRend.GetPropertyBlock (prop);
                prop.SetColor (ColorIDProp, value);
                mRend.SetPropertyBlock (prop);
            }
        }

        [SerializeField] Color _color = Color.white;
        [Range (0, 1)]
        [SerializeField] float _opacity = 1f;
        [SerializeField, HideInInspector] MeshRenderer mRend;
        [SerializeField, HideInInspector] Rotator rotator;
        [SerializeField, HideInInspector] SpawnedObject spawnedObject;
#if UNITY_EDITOR

        private void OnValidate ()
        {
            mRend = GetComponent<MeshRenderer> ();

            OpacityIDProp = Shader.PropertyToID ("_Opacity");
            ColorIDProp = Shader.PropertyToID ("_Color");

            if (prop == null) prop = new MaterialPropertyBlock ();

            rotator = GetComponent<Rotator> ();
            spawnedObject = GetComponent<SpawnedObject> ();
        }
#endif
        void Awake ()
        {
            prop = new MaterialPropertyBlock ();
            color = Color.white;
            Opacity = 0f;
        }
        EffectDataPreset preset;
        float FadeInTime => preset?preset.FadeInTime : 0f;
        float FadeOutTime => preset?preset.FadeOutTime : 0f;
        float LifeTime => preset?preset.LifeTime : 0f;
        public void InitSkin (EffectDataPreset preset)
        {

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
                this.DelayAction (LifeTime, FadeOut);
            }
            else
            {
                FadeOut ();
            }
        }
        void FadeOut ()
        {
            if (FadeOutTime != 0)
            {
                StopAllCoroutines ();
                FadeOut (FadeOutTime, PushToPool);
            }
            else
            {
                Opacity = 0;
                PushToPool ();
            }
        }

        public void PushToPool ()
        {
            StopAllCoroutines ();
            spawnedObject.PushToPool ();
        }

        public void FadeIn (float duration = 1f, Action onComplete = null) => DOVirtualFloat (0f, 1f, duration, value => Opacity = value, onComplete);
        public void FadeOut (float duration = 1f, Action onComplete = null) => DOVirtualFloat (1f, 0f, duration, value => Opacity = value, onComplete);
        void DOVirtualFloat (float startValue, float endValue, float duration, Action<float> action, Action onComplete = null)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine (cr_DOVirtualFloat (startValue, endValue, duration, action, onComplete));
        }
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
            yield break;
        }
#if UNITY_EDITOR
        private void Update ()
        {
            if (!Application.isPlaying)
            {
                color = _color;
                Opacity = _opacity;
            }
        }
#endif
    }
}
