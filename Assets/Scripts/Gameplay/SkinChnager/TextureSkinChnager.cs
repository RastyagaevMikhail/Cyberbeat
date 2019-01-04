using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class TextureSkinChnager : SkinChnager
    {
        [SerializeField] Material material;
        public Texture texture { set { material.SetTexture (TextureName, value); } }

        [SerializeField] string TextureName = "_MainTex";
        protected override void ApplySkin (SkinItem skinItem)
        {
            var skinTexture = skinItem.Prefab as Texture;
            if (skinTexture)
                texture = skinTexture;
        }
    }
}
