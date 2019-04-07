
using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "FlaotProperty", menuName = "GameCore/PropertyBlockInfo/Flaot")]
    public class FloatProperty : PropertyBlockInfo<float>
    {
        public override void OnUpdate (Renderer rend)
        {
            base.OnUpdate (rend);
            rend.GetPropertyBlock (propBlock);
            propBlock.SetFloat (namehash, value);
            rend.SetPropertyBlock (propBlock);
        }
    }

}
