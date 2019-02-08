using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[System.Serializable]
	public abstract class VariableReference<TVariable, TValue> where TVariable : SavableVariable<TValue>
	{
		public bool UseConstant;
		[ShowIf ("UseConstant")]
		public TValue ConstantValue;
		[HideIf ("UseConstant")]
		public TVariable Variable;
		public TValue ValueFast
		{
			get { return !this.UseConstant && this.Variable ? this.Variable.ValueFast : this.ConstantValue; }
		}
		public TValue Value
		{
			get { return !this.UseConstant && this.Variable ? this.Variable.Value : this.ConstantValue; }

		}

		private string GetValueString ()
		{
			return this.ConstantValue.ToString () + "    ";
		}

	}
}
