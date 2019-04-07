using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "TextureProperty", menuName = "GameCore/PropertyBlockInfo/Texture")]
    public class TextureProperty : PropertyBlockInfo<Texture>
    {
        public override Texture property
        {
            get
            {
                if (renderer == null) return null;
                renderer.GetPropertyBlock (propBlock);
                return propBlock.GetTexture (namehash);
            }
            set
            {
                if (renderer == null) return;
                renderer.GetPropertyBlock (propBlock);
                propBlock.SetTexture (namehash, value);
                renderer.SetPropertyBlock (propBlock);
            }
        }
    }

}
