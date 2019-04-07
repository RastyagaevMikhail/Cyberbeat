using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "ColorProperty", menuName = "GameCore/PropertyBlockInfo/Color")]
    public class ColorProperty : PropertyBlockInfo<Color>
    {
        public override Color property
        {
            get
            {
                if (renderer == null) return Color.white;
                renderer.GetPropertyBlock (propBlock);
                return propBlock.GetColor (namehash);
            }
            set
            {
                if (renderer == null) return;
                renderer.GetPropertyBlock (propBlock);
                propBlock.SetColor (namehash, value);
                renderer.SetPropertyBlock (propBlock);
            }
        }
        public ColorVariable color { set => property = value.Value; }
    }

}
