using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[ExecuteInEditMode]
	public class IntVariableTextSetter : VariableTextSetter<IntVariable, int>
	{
		int OldValue;
		protected override void Awake ()
		{
			base.Awake ();
			OldValue = variable.Value;
		}

		protected override void OnValueChanged (int obj)
		{
			DOVirtual
				.Float (OldValue, variable.Value, .5f, value => text = string.Format (stringFormat, Mathf.Round (value)))
				.OnComplete (() => OldValue = variable.Value);

		}
	}
}
