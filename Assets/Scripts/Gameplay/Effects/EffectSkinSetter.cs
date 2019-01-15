using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public class EffectSkinSetter : MonoBehaviour
    {
        private SkinnedMeshRenderer _mRend = null;
        public SkinnedMeshRenderer mRend { get { if (_mRend == null) _mRend = GetComponent<SkinnedMeshRenderer> (); return _mRend; } }
        private Texture Texture { get { return mRend.sharedMaterial.GetTexture (TextureName); } set { mRend.sharedMaterial.SetTexture (TextureName, value); } }
        private Vector2 Tilling { get { return mRend.sharedMaterial.GetTextureScale (TextureName); } set { mRend.sharedMaterial.SetTextureScale (TextureName, value); } }
        public Color color { get { return mRend.sharedMaterial.GetColor (ColorName); } set { mRend.sharedMaterial.SetColor (ColorName, value); } }
        private int CountShapes { get { return mRend.sharedMesh.blendShapeCount; } }
        private List<int> ShapesIndxes { get { return Enumerable.Range (0, CountShapes).ToList (); } }

        [SerializeField] EffectSkinData data;
        [SerializeField] Shapes TestNameState;
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
            // mRend.sharedMaterial = Instantiate (mRend.sharedMaterial);
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
            Texture = data.texture;
            Tilling = data.States[shape].Tilling;

            Enums.shapes
                .ForEach (s => SetWeight ((int) s, 0f));

            Debug.Log (Tools.LogCollection (Enums.shapes));
            if (shape != DefaultShape)
                SetWeight ((int) shape, 100f);

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

        public void InitSkin (EffectSkinData data, Shapes shape)
        {
            this.data = data;
            MoveState (shape);
            // Color newColor = color;
            // newColor.a = 0;
            // mRend.sharedMaterial.SetColor ("_Color", newColor);
            // mRend.sharedMaterial.DOFade (1, durationTransition);
        }
    }
    public enum Shapes
    {
        Circle = -1,
        Quad = 0,
        Triangle = 1,
        Pentagon = 2,
        Heptagon = 3,
    }
}
