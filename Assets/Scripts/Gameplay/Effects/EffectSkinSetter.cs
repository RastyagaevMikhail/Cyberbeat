using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public class EffectSkinSetter : TransformObject
    {
        private Transform _transform = null;
        public new Transform transform { get { if (_transform == null) _transform = GetComponent<Transform> (); return _transform; } }
        private SkinnedMeshRenderer _mRend = null;
        public SkinnedMeshRenderer mRend { get { if (_mRend == null) _mRend = GetComponent<SkinnedMeshRenderer> (); return _mRend; } }
        private Texture _texture = null;
        private Texture Texture
        {
            get
            {
                if (_texture == null) _texture = mRend.sharedMaterial.GetTexture (TextureName);
                return _texture;
            }
            set
            {
                if (_texture != value)
                {
                    _texture = value;
                    mRend.sharedMaterial.SetTexture (TextureName, value);
                }
            }
        }
        private Vector2 _tilling;
        private Vector2 Tilling
        {
            get
            {
                if (_tilling == Vector2.zero) _tilling = mRend.sharedMaterial.GetTextureScale (TextureName);
                return _tilling;
            }
            set
            {
                if (_tilling != value)
                {
                    _tilling = value;
                    mRend.sharedMaterial.SetTextureScale (TextureName, value);
                }
            }
        }
        public float Opacity { get { return mRend.sharedMaterial.GetFloat ("_Opacity"); } set { mRend.sharedMaterial.SetFloat ("_Opacity", value); } }
        public Color color { get { return mRend.sharedMaterial.GetColor (ColorName); } set { mRend.sharedMaterial.SetColor (ColorName, value); } }
        private int CountShapes { get { return mRend.sharedMesh.blendShapeCount; } }
        private List<int> ShapesIndxes { get { return Enumerable.Range (0, CountShapes).ToList (); } }

        [SerializeField] EffectSkinData data;
        [SerializeField] Shapes TestNameState;
        [SerializeField] Rotator rotator;
        [SerializeField] string TextureName = "_Base";
        [SerializeField] string ColorName = "_Color";
        [SerializeField] float durationTransition = 1f;
        static Shapes DefaultShape = Enums.shapes.Last ();
        Shapes CurrentShape = DefaultShape;

        #region Get Set Methods
        private string GetShapeName (int i)
        {
            return mRend.sharedMesh.GetBlendShapeName (i);
        }
        private float GetWeight (int index)
        {
            return mRend.GetBlendShapeWeight (index);
        }
        private void SetWeight (int index, float value)
        {
            if (index == -1) return;
            mRend.SetBlendShapeWeight (index, value);
        }
        #endregion
        private void Awake ()
        {
            mRend.sharedMaterial = Instantiate (mRend.sharedMaterial);
            color = Color.white;
            Opacity = 0f;

        }

        [ContextMenu ("AddSate")]
        void AddSate ()
        {
            if (data.texture == null) data.texture = Texture;

            List<string> newNameStates = Enums.shapes
                .Where (shape => GetWeight ((int) shape) == 100f)
                .Select (s => s.ToString ()).ToList ();

            string newNameState = "";

            if (newNameStates.Count == 0)
                newNameState = DefaultShape.ToString ();
            else
                newNameState = newNameStates[0];

            data.states.Add (new EffectSkinData.State () { Name = newNameState, Tilling = this.Tilling });
        }

        [ContextMenu ("PrintNameSB")]
        void PrintNameSB ()
        {
            var stringLog = Tools.LogCollection (Enumerable.Range (0, CountShapes).Select (i => GetShapeName (i)));
            Debug.Log (stringLog);
        }

        [ContextMenu ("LoadState")]
        public void LoadState ()
        {
            SetShapeState (TestNameState);
        }

        public void SetShapeState (Shapes shape)
        {
            if (CurrentShape == shape) return;
            if (!Texture)
                Texture = data.texture;
            Vector2 newTilling = data.States[shape].Tilling;
            Tilling = newTilling;

            Enums.shapes
                .ForEach (s => SetWeight ((int) s, 0f));

            // Debug.Log (Tools.LogCollection (Enums.shapes));
            if (shape != DefaultShape)
                SetWeight ((int) shape, 100f);

            CurrentShape = shape;
        }

        public void MoveState (Shapes shape)
        {

            if (CurrentShape == shape) return;
            Texture = data.texture;
            var state = data.States[shape];

            var startTillingX = Tilling.x;

            DOVirtual.Float (
                startTillingX,
                state.Tilling.x,
                durationTransition,
                value => Tilling = new Vector2 (value, 1f));
            foreach (var index in ShapesIndxes)
            {
                if (GetWeight (index) == 100f)
                    DOVirtual.Float (100f, 0, durationTransition, value => SetWeight (index, value));
            }

            if (shape != DefaultShape)
                DOVirtual.Float (0f, 100f, durationTransition, value => SetWeight ((int) shape, value));

            CurrentShape = shape;
        }

        [ContextMenu ("MoveToCurrentState")]
        void MoveToCurrentState ()
        {
            MoveState (TestNameState);
        }

        public void InitSkin (EffectDataPreset preset)
        {
            data = preset.SkinData;
            // MoveState (preset.Shape);
            transform.position = transform.TransformPoint (preset.OffsetPosition);
            SetShapeState (preset.Shape);
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
