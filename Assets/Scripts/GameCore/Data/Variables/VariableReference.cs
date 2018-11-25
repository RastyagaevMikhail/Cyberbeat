using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

using UnityEngine;
namespace GameCore
{
	[System.Serializable]
	public abstract class VariableReference<T, K> where T : SavableVariable<K>
	{
		public bool UseConstant = true;
		[OdinSerialize]
		public K ConstantValue;
		[OdinSerialize]
		public SavableVariable<K> Variable;
		public K Value
		{
			get { return UseConstant ? ConstantValue : Variable.Value; }
			set
			{
				if (UseConstant) ConstantValue = value;
				else Variable.Value = value;
			}
		}

#if UNITY_EDITOR
		public abstract void DrawMe (Rect position);
#endif

	}
}
