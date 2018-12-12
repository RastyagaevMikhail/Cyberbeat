using System;

using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public class IntReference : VariableReference<IntVariable, int>
    {
        public static implicit operator int (IntReference reference)
        {
            return reference.Value;
        }

        public void Increment ()
        {
            if (UseConstant) ConstantValue++;
            else
                Variable.Increment ();
        }
        public void Decrement ()
        {
            if (UseConstant) ConstantValue--;
            else
                Variable.Decrement ();
        }
    }
}
