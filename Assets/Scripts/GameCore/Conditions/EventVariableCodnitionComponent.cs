using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public abstract class EventVariableCodnitionComponent<ValueType, VariableType> : MonoBehaviour
    where VariableType : SavableVariable<ValueType>
    {
        [SerializeField] VariableType variable;
        [SerializeField] UnityEvent onTrue;
        [SerializeField] UnityEvent onFalse;
        [SerializeField] bool debug;
        public void DoCondition (ValueType value)
        {
            bool conditionResult = value.Equals (variable.Value);
            string log = string.Empty;

            if (debug)
            {
                log += $"Variable {variable.name.so()} is {(conditionResult ? "Equals".green() :"NOT Equals".red())} value {value}\n";
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
