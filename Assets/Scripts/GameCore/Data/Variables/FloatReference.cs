using System;
using UnityEngine;

namespace GameCore
{
    [Serializable]
    public class FloatReference : VariableReference<FloatVariable, float>
    {

        public static implicit operator float (FloatReference reference)
        {
            return reference.Value;
        }
    }
}
