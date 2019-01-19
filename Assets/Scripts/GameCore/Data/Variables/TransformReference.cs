using UnityEngine;
namespace GameCore
{
    [System.Serializable]
    public class TransformReference : VariableReference<TransformVariable, Transform>
    {
        public static implicit operator Transform (TransformReference reference)
        {
            return reference.Value;
        }
    }
}
