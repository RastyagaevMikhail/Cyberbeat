using FluffyUnderware.Curvy.Controllers;

using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventListenerFloat))] //  UpdateSpeed (float speed)
	public class PlayerBot : MonoBehaviour
	{

		enum Dir { None, Left, Rigth }

		[SerializeField] MaterialSwitcherVariable matSwitch;
		[SerializeField] InputControllerComponentVariable inputControllerComponent;
		[SerializeField] TransformVariable Target;
		[SerializeField] bool bitAll;
		new Transform transform { get { return Target.ValueFast; } }
		Dir lastMove = Dir.None;
		[SerializeField] float collideBricksOffset = 0.5f;
		[SerializeField] float Radius = 1f;
		[SerializeField] float minRangeDistance = 0.5f;
		[SerializeField] float maxRangeDistance = 3.5f;
		[SerializeField] InputSettings inputSettings;

		//?ControlSwitchController.OnControlSwitched
		public void OnControlSwitched (InputControlType type)
		{
			AvoidForward ();
		}
		private void Awake ()
		{
			OnUpdateSpeed (50);
		}
		public void OnUpdateSpeed (float speed)
		{
			float S = inputSettings.width;
			float ts = inputSettings.SwipeDuration / 2f;
			float Vs = S / ts;
			Vector3 Vs_ = Vector3.right * S;
			Vector3 Vx_ = Vector3.forward * speed;
			Vector3 Vxs_ = Vs_ + Vx_;
			float Vxs = Vxs_.magnitude;
			float txs = ts;
			float Sxs = Vxs * txs;
			X = (S.Sqr () - Sxs.Sqr ()).Abs ().Sqrt ();
		}

		/// <summary>
		/// Дистанция для рейкаста вперед на упреждения движения бота
		/// </summary>
		[SerializeField] float X;
		[ShowInInspector] float distanceRaycast => (X - Radius / 2f - collideBricksOffset).Abs ().GetAsClamped (minRangeDistance, maxRangeDistance);
		private Ray rightRay;
		private Ray leftRay;
		private Ray forwardRay;

		private void Update ()
		{
			Vector3 forwardOffset = transform.forward * (distanceRaycast);
			Vector3 origin = transform.position /* + transform.up * 0.5f */ ;
			Vector3 rightOrigin = origin + transform.right * (inputSettings.width - 0.5f);
			rightRay = new Ray (rightOrigin, transform.forward);
			Vector3 leftOrigin = origin - transform.right * (inputSettings.width - 0.5f);
			leftRay = new Ray (leftOrigin, transform.forward);
			forwardRay = new Ray (origin, transform.forward);
			RaycastHit hit = new RaycastHit ();
			if (Physics.SphereCast (rightRay, Radius, out hit, distanceRaycast))
			{
				if (Checkhit (hit))
				{
					inputControllerComponent.MoveRight ();
					lastMove = Dir.Rigth;
				}
			}

			if (Physics.SphereCast (leftRay, Radius, out hit, distanceRaycast))
			{
				if (Checkhit (hit))
				{
					inputControllerComponent.MoveLeft ();
					lastMove = Dir.Left;
				}
			}
			// if (Physics.SphereCast (forwardRay, Radius, out hit, distanceRaycast))
			// {
			// 	GameObject hitGO = hit.transform.gameObject;
			// 	bool isBrick = hitGO.CompareTag ("Brick");
			// 	MaterialSwitcher materialSwitcher = hitGO.GetComponent<MaterialSwitcher> ();

			// 	if (isBrick && (materialSwitcher.CurrentColor != matSwitch.CurrentColor))
			// 	{
			// 		AvoidForward ();
			// 	}
			// }

		}

		bool Checkhit (RaycastHit hit)
		{
			GameObject hitGO = hit.transform.gameObject;
			bool isBit = hitGO.CompareTag ("Brick") || hitGO.CompareTag ("Switcher");
			if (!isBit) return false;
			if (bitAll) return true;
			MaterialSwitcher materialSwitcher = hitGO.GetComponent<MaterialSwitcher> ();
			bool isHit = isBit && (materialSwitcher.ChechColor (matSwitch.CurrentColor));
			return isHit;
		}

		private void AvoidForward ()
		{
			// (lastMove == Dir.Rigth ? (System.Action) inputControllerComponent.MoveLeft : inputControllerComponent.MoveRight) ();
		}
		private void OnDrawGizmos ()
		{

			DrawCast (rightRay, Color.red);
			DrawCast (leftRay, Color.green);

			DrawRay (rightRay, Color.red);
			DrawRay (leftRay, Color.green);
			// DrawRay (forwardRay, Color.blue);

		}

		private void DrawCast (Ray? ray, Color color)
		{

			if (ray != null)
			{
				Gizmos.color = color;
				Gizmos.DrawWireSphere (ray.Value.origin + ray.Value.direction.normalized * distanceRaycast, Radius);
			}
		}

		private void DrawRay (Ray? ray, Color color)
		{
			if (ray != null)
			{
				Gizmos.color = color;
				Gizmos.DrawRay (ray.Value.origin, ray.Value.direction.normalized * distanceRaycast);
			}
		}
	}
}
