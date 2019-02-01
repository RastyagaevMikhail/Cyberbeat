using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Condition/More/Int")]
    public class IntMoreCondition : ACondition
    {
        [SerializeField] IntReference first;
        [Header (">")]
        [SerializeField] IntReference second;
        public override bool Value => first.ValueFast > second.ValueFast;
    }
}
