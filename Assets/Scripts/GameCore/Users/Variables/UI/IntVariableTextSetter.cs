using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[ExecuteInEditMode]
	public class IntVariableTextSetter : VariableTextSetter<IntVariable, int>
	{
		[SerializeField] int OldValue;
		protected void Awake ()
		{
			OldValue = variable.Value;
		}

		protected override void OnValueChanged (int newValue)
		{
			DOVirtual
				.Float (OldValue, newValue, .5f, value => text = string.Format (stringFormat, Mathf.Round (value)))
				.OnComplete (() => OldValue = variable.Value);
		}
        
    }
}
