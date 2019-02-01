using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class ConditionComponent : MonoBehaviour
    {
        [SerializeField] ACondition condition;
        [SerializeField] UnityEvent onTrue;
        [SerializeField] UnityEvent onFalse;

        public void DoCondition ()
        {
            (condition.Value ?(UnityAction) onTrue.Invoke : onFalse.Invoke) ();
        }
    }
}
