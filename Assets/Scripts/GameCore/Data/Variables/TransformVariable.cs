using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "TransformVariable", menuName = "GameCore/Variable/Transform")]
    public class TransformVariable : SavableVariable<Transform>
    {
        public Transform parent { get { return Value ? Value.parent : null; } set { Value = value; } }
    }
}
