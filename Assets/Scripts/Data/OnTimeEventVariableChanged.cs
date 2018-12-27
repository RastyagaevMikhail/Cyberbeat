using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{

	public class OnTimeEventVariableChanged : MonoBehaviour
	{

		[SerializeField] TimeEventVariable variable;
		[SerializeField] UnityEventTimeEvent action;
		[SerializeField] UnityEventBool action_bool;

		private void OnEnable ()
		{
			variable.OnValueChanged += action.Invoke;
			variable.OnValueChanged += OnValuechngeFromBool;
		}

		private void OnDisable ()
		{
			variable.OnValueChanged -= action.Invoke;
			variable.OnValueChanged -= OnValuechngeFromBool;
		}
		private void OnValuechngeFromBool (TimeEvent obj)
		{
			action_bool.Invoke (obj.isTime);
		}

	}

	[System.Serializable] public class UnityEventTimeEvent : UnityEvent<TimeEvent> { }
}
