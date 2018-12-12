using UnityEngine;

namespace GameCore
{
    public class Vector3Reference : VariableReference<Vector3Variable, Vector3>
    {

        public static implicit operator Vector3 (Vector3Reference varivable)
        {
            return varivable.Value;
        }
    }
}
