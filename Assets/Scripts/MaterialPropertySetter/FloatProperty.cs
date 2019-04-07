using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu (fileName = "FlaotProperty", menuName = "GameCore/PropertyBlockInfo/Flaot")]
    public class FloatProperty : PropertyBlockInfo<float>
    {
        public override float property
        {
            get
            {
                if (renderer == null) return 0;
                renderer.GetPropertyBlock (propBlock);
                return propBlock.GetFloat (namehash);
            }
            set
            {
                if (renderer == null) return;
                renderer.GetPropertyBlock (propBlock);
                propBlock.SetFloat (namehash, value);
                renderer.SetPropertyBlock (propBlock);
            }
        }
    }

}
