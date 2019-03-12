using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "TransformVariable", menuName = "GameCore/Variable/Transform")]
    public class TransformVariable : SavableVariable<Transform>
    {
        public Transform parent { get { return Value ? Value.parent : null; } }
        public void SetParentFrom (Transform transform)
        {
            transform.SetParent (Value);
        }
        public void SetParentAs (Transform transform)
        {
            Value.SetParent(transform);
        }
         public static implicit operator Transform (TransformVariable variable)
        {
            return variable.Value;
        }
    }
}
