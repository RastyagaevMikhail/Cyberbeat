using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "TransformVariable", menuName = "GameCore/Variable/Transform")]
    public class TransformVariable : SavableVariable<Transform>
    {
        public Transform parent { get { return ValueFast ? ValueFast.parent : null; } }
        public void SetParentFrom (Transform transform)
        {
            transform.SetParent (ValueFast);
        }
        public void SetParentAs (Transform transform)
        {
            ValueFast.SetParent(transform);
        }
    }
}
