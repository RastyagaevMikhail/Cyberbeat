using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "ColorProperty", menuName = "GameCore/PropertyBlockInfo/Color")]
    public class ColorProperty : PropertyBlockInfo<Color>
    {
        public override void OnUpdate (Renderer rend)
        {
            base.OnUpdate (rend);
            rend.GetPropertyBlock (propBlock);
            propBlock.SetColor (namehash, value);
            rend.SetPropertyBlock (propBlock);
        }
    }
}
