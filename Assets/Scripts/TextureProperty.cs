using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "TextureProperty", menuName = "GameCore/PropertyBlockInfo/Texture")]
    public class TextureProperty : PropertyBlockInfo<Texture>
    {
        public override void OnUpdate (Renderer rend)
        {
            base.OnUpdate (rend);
            rend.GetPropertyBlock (propBlock);
            propBlock.SetTexture (namehash, value);
            rend.SetPropertyBlock (propBlock);
        }
    }

}
