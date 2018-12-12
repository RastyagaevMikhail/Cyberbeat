using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class OnFloatVariableChanged : MonoBehaviour
	{

		[SerializeField] FloatVariable variable;
		[Sirenix.OdinInspector.DrawWithUnity]
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

	[System.Serializable] public class UnityEventFloat : UnityEvent<float> { }
}
