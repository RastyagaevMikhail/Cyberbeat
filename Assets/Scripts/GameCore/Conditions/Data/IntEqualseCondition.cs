using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Condition/Equalse/Int")]
    public class IntEqualseCondition : ACondition 
    {
        [SerializeField] IntReference first;
        [Header ("==")]
        [SerializeField] IntReference second;
        public override bool Value => first.ValueFast == second.ValueFast;
    }
}
