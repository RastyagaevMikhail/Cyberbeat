using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (InputControllerComponent))]
	public class ControlSwitchController : TimeEventsCatcher
	{

		private InputControllerComponent _inputControllerComponent = null;
		public InputControllerComponent inputControllerComponent { get { if (_inputControllerComponent == null) _inputControllerComponent = GetComponent<InputControllerComponent> (); return _inputControllerComponent; } }

		[SerializeField] UnityEventInputControlType OnSwitchControl;

		public override void _OnChanged (TimeEvent timeEvent)
		{
			if (timeEvent.isTime && timeEvent.timeOfEvent.Start == 0) return;
			InputControlType controlTypeToSwitch = timeEvent.isTime ? InputControlType.Center : InputControlType.Side;
			inputControllerComponent.SetControl (controlTypeToSwitch);
			OnSwitchControl.Invoke (controlTypeToSwitch);
		}
	}
}
