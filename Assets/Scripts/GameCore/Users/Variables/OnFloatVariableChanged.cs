using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class OnFloatVariableChanged : MonoBehaviour
	{

		[SerializeField] FloatVariable variable;
		[SerializeField] UnityEventFloat action;
		private void OnEnable ()
		{
			variable.OnValueChanged += action.Invoke;
		}
		private void OnDisable ()
		{
			variable.OnValueChanged -= action.Invoke;
		}
	}
}
