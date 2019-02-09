using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Condition/TimeSpan/IsZero")]
    public class TimeSpanIsZeroCondition : ACondition
    {
        [SerializeField] TimeSpanVariable variable;
        public override bool Value => variable.IsZero ;
    }
}
