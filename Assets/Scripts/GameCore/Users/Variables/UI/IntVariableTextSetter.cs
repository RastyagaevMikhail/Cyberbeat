using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore {
	[ExecuteInEditMode]
	public class IntVariableTextSetter : VariableTextSetter<IntVariable, int> {

		protected override void OnValueChanged(int newValue) {
			text = string.Format(stringFormat, newValue);
		}
	}
}
