using System;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[RequireComponent (typeof (Renderer))]
    [ExecuteInEditMode]
    public class InEditorMaterialPropertyBlockSetter : MonoBehaviour
    {
        [SerializeField] List<FloatProperty> floats;
        [SerializeField] List<ColorProperty> colors;
        [SerializeField] List<TextureProperty> textures;
        [HideInInspector]
        [SerializeField] Renderer rend;
        private void OnValidate ()
        {
            ValidateRenderer ();
            ValidateFloats ();
            ValidateColors ();
            ValidateTextures ();
        }

        private void ValidateRenderer ()
        {
            if (rend == null)
                rend = GetComponent<Renderer> ();
        }

        private void ValidateColors ()
        {
            foreach (var prop in colors)
                prop.OnValidte ();
        }
        private void ValidateFloats ()
        {
            foreach (var prop in floats)
                prop.OnValidte ();
        }
        private void ValidateTextures ()
        {
            foreach (var prop in textures)
                prop.OnValidte ();
        }

        private void Update ()
        {
            if (!Application.isPlaying)
            {
                UpdateFloats ();
                UpdateColors ();
                UpdateTextures ();
            }
        }

        private void UpdateColors ()
        {
            foreach (var prop in colors)
                prop.OnUpdate (rend);
        }
        private void UpdateFloats ()
        {
            foreach (var prop in floats)
                prop.OnUpdate (rend);
        }
        private void UpdateTextures ()
        {
            foreach (var prop in textures)
                prop.OnUpdate (rend);
        }

        [ContextMenu ("Add Flaot")]
        void AddFlaot ()
        {
            floats.Add (PropertyBlockInfo.CreateInstance<FloatProperty> ());
        }

        [ContextMenu ("Add Color")]
        void AddColor ()
        {
            colors.Add (PropertyBlockInfo.CreateInstance<ColorProperty> ());
        }

        [ContextMenu ("Add Texture")]
        void AddTexture ()
        {
            textures.Add (PropertyBlockInfo.CreateInstance<TextureProperty> ());
        }
    }
    public class PropertyBlockInfo : ScriptableObject
    {
        protected MaterialPropertyBlock propBlock = null;
        [HideInInspector]
        [SerializeField] protected int namehash = 0;
        [SerializeField] protected string Name;
        public void OnValidte ()
        {
            ValidateHash ();
            ValidateBlock ();
        }
        private void ValidateHash ()
        {
            if (namehash == 0)
                namehash = Shader.PropertyToID (Name);
        }

        protected void ValidateBlock ()
        {
            if (propBlock == null)
                propBlock = new MaterialPropertyBlock ();
        }

        public virtual void OnUpdate (Renderer Renderer)
        {
            ValidateBlock ();
        }
    }
    public abstract class PropertyBlockInfo<T> : PropertyBlockInfo
    {
        [SerializeField] protected T value = default (T);
    }

}
