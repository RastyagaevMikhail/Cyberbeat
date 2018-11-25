using System;

using UnityEngine;

namespace GameCore
{
    [System.Serializable]
    public class IntReference : VariableReference<IntVariable, int>
    {
#if UNITY_EDITOR
        public override void DrawMe (Rect position)
        {
            if (UseConstant) ConstantValue = UnityEditor.EditorGUI.IntField (position, Value);
            else Variable = UnityEditor.EditorGUI.ObjectField (position, Variable, typeof (IntVariable), false) as IntVariable;
        }
#endif

        public static implicit operator int (IntReference reference)
        {
            return reference.Value;
        }

        public void Increment ()
        {
            Value++;
        }
        public void Decrement ()
        {
            Value--;
        }
    }
}
