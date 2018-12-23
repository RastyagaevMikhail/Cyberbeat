using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class OnBoolVariableChanged : MonoBehaviour
	{

		[SerializeField] BoolVariable variable;
		[Sirenix.OdinInspector.DrawWithUnity]
		[SerializeField] UnityEventBool action;
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
