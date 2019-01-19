using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class PlayerBot : MonoBehaviour
	{
		enum Dir { None, Left, Rigth }
		private Player _player = null;
		public Player player { get { return _player ?? (_player = GetComponent<Player> ()); } }
		private SpeedTimeUser _speedTimeUser = null;
		public SpeedTimeUser speedTimeUser { get { return _speedTimeUser ?? (_speedTimeUser = GetComponentInParent<SpeedTimeUser> ()); } }
		Dir lastMove = Dir.None;
		const float maxDistance = 6f;
		private const float Radius = 1f;

		private void Awake ()
		{
			ControlSwitchController.OnSwitchControl += OnControlSwitched;
			speedTimeUser.OnSpeedUpdated += UpdateSpeed;
		}
		private void OnDisable ()
		{
			ControlSwitchController.OnSwitchControl -= OnControlSwitched;
			speedTimeUser.OnSpeedUpdated -= UpdateSpeed;
		}
		private void OnControlSwitched (InputControlType type)
		{
			AvoidForward ();
		}

		private void UpdateSpeed (float speed)
		{
			float width = player.inputSettings.width;
			float swipeDuration = player.inputSettings.SwipeDuration;
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
					player.MoveRight ();
					lastMove = Dir.Rigth;
				}
			}

			if (Physics.SphereCast (leftRay, Radius, out hit, maxDistance))
			{
				if (Checkhit (hit))
				{
					player.MoveLeft ();
					lastMove = Dir.Left;
				}
			}
			if (Physics.SphereCast (forwardRay, Radius, out hit, maxDistance))
			{
				GameObject hitGO = hit.transform.gameObject;
				bool isBrick = hitGO.CompareTag ("Brick");
				MaterialSwitcher materialSwitcher = hitGO.GetComponent<MaterialSwitcher> ();

				if (isBrick && (!materialSwitcher.Constant || materialSwitcher.CurrentColor != player.matSwitch.CurrentColor))
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
			return isBit && (materialSwitcher.Constant || materialSwitcher.CurrentColor == player.matSwitch.CurrentColor);
		}

		private void AvoidForward ()
		{
			(lastMove == Dir.Rigth ? (System.Action) player.MoveLeft : player.MoveRight) ();
		}
		public void SwithchEnabled ()
		{
			enabled = !enabled;
		}
	}
}
