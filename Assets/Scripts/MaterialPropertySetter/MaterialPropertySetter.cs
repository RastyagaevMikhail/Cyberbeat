using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [RequireComponent (typeof (Renderer))]
    [ExecuteInEditMode]
    public class MaterialPropertySetter : MonoBehaviour
    {

        [SerializeField] List<FloatProperty> floats;
        [SerializeField] List<ColorProperty> colors;
        [SerializeField] List<TextureProperty> textures;
        // [HideInInspector]
        [SerializeField] Renderer rend;
        private void OnValidate ()
        {
            ValidateRenderer ();
            ValidateProperties (floats);
            ValidateProperties (colors);
            ValidateProperties (textures);
        }

        private void ValidateRenderer ()
        {
            if (rend == null)
                rend = GetComponent<Renderer> ();
        }
        void ValidateProperties<T> (List<T> properties) where T : PropertyBlockInfo
        {
            foreach (var prop in properties)
            {
                prop.OnValidte ();
                prop.Init (rend);
            }
        }
        private void Awake ()
        {
            OnValidate ();
        }

        private void Update ()
        {
            if (!Application.isPlaying)
            {
                OnValidate ();
                UpdateProperties (floats);
                UpdateProperties (colors);
                UpdateProperties (textures);
            }
        }
        void UpdateProperties<T> (List<T> properties) where T : PropertyBlockInfo
        {
            foreach (var prop in properties)
                prop.OnUpdateInEditor ();
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
            Debug.LogFormat ("textures = {0}", textures);
            textures.Add (PropertyBlockInfo.CreateInstance<TextureProperty> ());
        }

    }
}
