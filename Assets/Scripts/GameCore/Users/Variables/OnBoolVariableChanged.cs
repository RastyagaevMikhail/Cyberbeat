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

		private void OnEnable ()
		{
			variable.OnValueChanged += action.Invoke;
			if (updateOnEnable) action.Invoke (variable.Value);
		}
		private void OnDisable ()
		{
			variable.OnValueChanged -= action.Invoke;
		}
	}
}
