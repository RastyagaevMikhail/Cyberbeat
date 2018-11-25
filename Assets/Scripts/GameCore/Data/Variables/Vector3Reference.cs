using UnityEngine;

namespace GameCore
{
    public class Vector3Reference : VariableReference<Vector3Variable, Vector3>
    {
#if UNITY_EDITOR
        public override void DrawMe (Rect position)
        {
            if (UseConstant) ConstantValue = UnityEditor.EditorGUI.Vector3Field (position, "", ConstantValue);
            else Variable = UnityEditor.EditorGUI.ObjectField (position, Variable, typeof (Vector3Variable), false) as Vector3Variable;
        }
#endif
        public static implicit operator Vector3 (Vector3Reference varivable)
        {
            return varivable.Value;
        }
    }
}
