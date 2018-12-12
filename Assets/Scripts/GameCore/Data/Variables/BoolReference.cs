using UnityEngine;

namespace GameCore
{
	public class BoolReference : VariableReference<BoolVariable, bool>
	{
		public static implicit operator bool (BoolReference reference)
		{
			return reference.Value;
		}
	}
}
