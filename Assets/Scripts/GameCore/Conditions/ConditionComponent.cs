using Sirenix.OdinInspector;

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
        [SerializeField] UnityEventBool onCondition;
        [SerializeField] UnityEventBool onInverseCondition;
        [SerializeField] bool debug;
        [Button]
        public void DoCondition (bool value)
        {
            string log = string.Empty;

            if (debug)
            {
                log += $"Condition {condition.name.so()} is {(value ?value.ToString().green() : value.ToString().red())} \n";
            }
            if (value)
            {
                onTrue.Invoke ();
                if (debug) log += $"{onTrue.Log ()} \n";
            }
            else
            {
                onFalse.Invoke ();
                if (debug) log += $"{onFalse.Log ()} \n";
            }
            onCondition.Invoke (value);
            onInverseCondition.Invoke (!value);
            if (debug) Debug.Log (log);
        }
        public void DoCondition ()
        {
            DoCondition (condition.Value);
        }

    }
}
