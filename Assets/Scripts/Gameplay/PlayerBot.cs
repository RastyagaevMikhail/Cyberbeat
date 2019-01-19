using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventFloat))] //  UpdateSpeed (float speed)
	public class PlayerBot : MonoBehaviour
	{
		enum Dir { None, Left, Rigth }
		private MaterialSwitcher _matSwitch;
		public MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponentInChildren<MaterialSwitcher> (); } return _matSwitch; } } private InputControllerComponent _inputControllerComponent = null;
		public InputControllerComponent inputControllerComponent
		{
			get
			{
				if (_inputControllerComponent == null)
					_inputControllerComponent = GetComponent<InputControllerComponent> ();
				return _inputControllerComponent;
			}
		}
		private SpeedTimeUser _speedTimeUser = null;
		public SpeedTimeUser speedTimeUser { get { return _speedTimeUser ?? (_speedTimeUser = GetComponentInParent<SpeedTimeUser> ()); } }
		Dir lastMove = Dir.None;
		const float maxDistance = 6f;
		private const float Radius = 1f;

		//?ControlSwitchController.OnControlSwitched
		public void OnControlSwitched (InputControlType type)
		{
			AvoidForward ();
		}

		public void UpdateSpeed (float speed)
		{
			float width = inputControllerComponent.inputSettings.width;
			float swipeDuration = inputControllerComponent.inputSettings.SwipeDuration;
			float time = swipeDuration / 2f;
			float Vw = width * time;
			float Swv = (speed.Sqr () + Vw.Sqr ()).Sqrt ();
			float Sws = Swv * time;
			float SwsSqr = Sws.Sqr ();
			float widthSqr = width.Sqr ();
			float Diff = (SwsSqr - widthSqr);
			Fs = Diff.Abs ().Sqrt () - Time.deltaTime * speed;
		}
		float Fs;
		private void FixedUpdate ()
		{
			Vector3 forwardOffset = transform.forward * (Fs);
			Vector3 origin = transform.position + transform.up * 0.5f + forwardOffset;
			Ray rightRay = new Ray (origin, transform.right);
			Ray leftRay = new Ray (origin, -transform.right);
			Ray forwardRay = new Ray (origin, transform.forward);
			RaycastHit hit = new RaycastHit ();
			if (Physics.SphereCast (rightRay, Radius, out hit, maxDistance))
			{
				if (Checkhit (hit))
				{
					inputControllerComponent.MoveRight ();
					lastMove = Dir.Rigth;
				}
			}

			if (Physics.SphereCast (leftRay, Radius, out hit, maxDistance))
			{
				if (Checkhit (hit))
				{
					inputControllerComponent.MoveLeft ();
					lastMove = Dir.Left;
				}
			}
			if (Physics.SphereCast (forwardRay, Radius, out hit, maxDistance))
			{
				GameObject hitGO = hit.transform.gameObject;
				bool isBrick = hitGO.CompareTag ("Brick");
				MaterialSwitcher materialSwitcher = hitGO.GetComponent<MaterialSwitcher> ();

				if (isBrick && (!materialSwitcher.Constant || materialSwitcher.CurrentColor != matSwitch.CurrentColor))
				{
					AvoidForward ();
				}
			}

		}

		bool Checkhit (RaycastHit hit)
		{
			GameObject hitGO = hit.transform.gameObject;
			bool isBit = hitGO.CompareTag ("Brick") || hitGO.CompareTag ("Switcher");
			if (!isBit) return false;
			MaterialSwitcher materialSwitcher = hitGO.GetComponent<MaterialSwitcher> ();
			return isBit && (materialSwitcher.Constant || materialSwitcher.CurrentColor == matSwitch.CurrentColor);
		}

		private void AvoidForward ()
		{
			(lastMove == Dir.Rigth ? (System.Action) inputControllerComponent.MoveLeft : inputControllerComponent.MoveRight) ();
		}
	}
}
