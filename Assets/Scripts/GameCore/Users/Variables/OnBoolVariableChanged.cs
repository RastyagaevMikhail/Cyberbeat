using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class OnBoolVariableChanged : MonoBehaviour
	{
		[SerializeField] BoolVariable variable;
		[SerializeField] UnityEventBool action;
		[SerializeField] bool updateOnEnable;
		[SerializeField] bool Inverse;

		private void OnEnable ()
		{
			variable.OnValueChanged += OnInvoke;
			if (updateOnEnable) OnInvoke (variable.Value);
		}

		private void OnInvoke (bool value)
		{
			action.Invoke (Inverse ? !value : value);
		}

		private void OnDisable ()
		{
			variable.OnValueChanged -= OnInvoke;
		}
	}
}
