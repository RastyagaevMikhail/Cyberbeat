using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class OnIntVariableChanged : MonoBehaviour
	{
		[SerializeField] IntVariable variable;
		[SerializeField, DrawWithUnity] UnityEventInt action;

		private void OnEnable ()
		{
			variable.OnValueChanged += action.Invoke;
		}
		private void OnDisable ()
		{
			variable.OnValueChanged -= action.Invoke;
		}
	}

	[System.Serializable] public class UnityEventInt : UnityEvent<int> { }
}
