using System;
using UnityEngine;

namespace GameCore
{
    [Serializable]
    public class FloatReference : VariableReference<FloatVariable, float>
    {
#if UNITY_EDITOR
        public override void DrawMe (Rect position)
        {
            if (UseConstant) ConstantValue = UnityEditor.EditorGUI.FloatField (position, "", ConstantValue);
            else Variable = UnityEditor.EditorGUI.ObjectField (position, Variable, typeof (FloatVariable), false) as FloatVariable;
        }
#endif
        public static implicit operator float (FloatReference reference)
        {
            return reference.Value;
        }
    }
}
