using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "DateTimeLessNowInTimeSpanInterval", menuName = "GameCore/Condition/Less/DateTime/Now In TimeSpan Interval")]
    public class DateTimeLessNowInTimeSpanIntervalCondition : ACondition
    {
        [SerializeField] DateTimeVariable dateTimeVariable;
        [SerializeField] TimeSpanVariable timeSpanVariable;
        public override bool Value => (dateTimeVariable.Value + timeSpanVariable.Value) < DateTime.Now;
        [MultiLineProperty(5)]
        [ShowInInspector] string value => $"{Value} \n" +
            $"{dateTimeVariable.Value} + {timeSpanVariable.Value} = \n\n{dateTimeVariable.Value + timeSpanVariable.Value} <\n{DateTime.Now}";
    }
}
