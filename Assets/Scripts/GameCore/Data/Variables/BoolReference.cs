using UnityEngine;

namespace GameCore
{
	public class BoolReference : VariableReference<BoolVariable, bool>
	{
#if UNITY_EDITOR
		public override void DrawMe (Rect position)
		{
			if (UseConstant) ConstantValue = UnityEditor.EditorGUI.Toggle (position, "", ConstantValue);
			else Variable = UnityEditor.EditorGUI.ObjectField (position, Variable, typeof (BoolVariable), false) as BoolVariable;
		}
#endif
		public static implicit operator bool (BoolReference reference)
		{
			return reference.Value;
		}
	}
}
