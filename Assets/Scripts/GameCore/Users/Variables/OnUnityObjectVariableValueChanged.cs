
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[System.Serializable] public class ObjectUnityEvent : UnityEvent<Object> { }
	public class OnUnityObjectVariableValueChanged : MonoBehaviour
	{
		[SerializeField] UnityObjectVariable variable;
		
		[SerializeField] ObjectUnityEvent action = new ObjectUnityEvent ();
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
