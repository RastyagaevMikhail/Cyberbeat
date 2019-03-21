using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Condition/More/Float")]
    public class FloatMoreCondintion : ACondition
    {
        [SerializeField] FloatReference first;
        [Header (">")]
        [SerializeField] FloatReference second;
        public override bool Value => first.ValueFast > second.ValueFast;
    }
}

