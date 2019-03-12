using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(menuName = "GameCore/Condition/BoolVariable")]
    public class BoolVariabaleCondition : ACondition
    {
        [SerializeField] BoolVariable variable;
        public override bool Value
        {
            get
            {
                return variable.Value;
            }
        }
    }
}
