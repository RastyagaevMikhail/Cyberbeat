using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[ExecuteInEditMode]
	public class IntVariableTextSetter : VariableTextSetter<IntVariable, int>
	{
		[SerializeField] int OldValue;
		[SerializeField] UnityEvent OnComplete;
		protected void Awake ()
		{
			OldValue = variable.Value;
		}

		protected override void OnValueChanged (int newValue)
		{
			if (newValue == OldValue)
			{
				text = string.Format (stringFormat, Mathf.Round (newValue));
				return;
			}
			DOVirtual
				.Float (OldValue, newValue, .5f, value => text = string.Format (stringFormat, Mathf.Round (value)))
				.OnComplete (onComplete);
		}
		void onComplete ()
		{
			OldValue = variable.Value;
			OnComplete.Invoke ();
		}
#if UNITY_EDITOR

		private void Update ()
		{
			if (!Application.isPlaying) OldValue = variable.Value;
		}
#endif

	}
}
