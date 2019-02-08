using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Condition/Less/Int")]
    public class IntLessCondition : ACondition
    {
        [SerializeField] IntReference first;
        [Header ("<")]
        [SerializeField] IntReference second;
        public override bool Value => first.ValueFast > second.ValueFast;
    }
}
