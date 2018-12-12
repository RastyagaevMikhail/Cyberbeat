using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[System.Serializable]
	public abstract class VariableReference<TVariable, TValue> where TVariable : SavableVariable<TValue>
	{
		public bool UseConstant;

		public TValue ConstantValue;

		[SuffixLabel ("$GetValueString", true)]
		public TVariable Variable;

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
