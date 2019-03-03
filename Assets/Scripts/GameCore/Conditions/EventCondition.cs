using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public abstract class EventCondition<ValueType> : MonoBehaviour
    {
        [SerializeField] ValueType comparisonValue;
        [SerializeField] UnityEvent onTrue;
        [SerializeField] UnityEvent onFalse;
        [SerializeField] bool debug;
        public void DoCondition (ValueType value)
        {
            bool conditionResult = value.Equals (comparisonValue);
            string log = string.Empty;

            if (debug)
            {
                log += $"Variable {comparisonValue.ToString().so()} is {(conditionResult ? "Equals".green() :"NOT Equals".red())} value {value}\n";
            }
            if (conditionResult)
            {
                onTrue.Invoke ();
                if (debug) log += $"{onTrue.Log ()} \n";
            }
            else
            {
                onFalse.Invoke ();
                if (debug) log += $"{onFalse.Log ()} \n";
            }
            if (debug) Debug.Log (log, this);
        }

    }
}
