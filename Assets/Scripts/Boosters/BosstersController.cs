using System;
using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

using GameCore;
namespace CyberBeat
{
	public class BosstersController : TransformObject
	{
		[SerializeField] EventListener OnBoosterTakedListener;
		[SerializeField] GameEventUnityObject OnBoosterStart;
		[SerializeField] RuntimeTimerView timerView;
		BoostersData boostersData { get { return BoostersData.instance; } }
		Pool pool { get { return Pool.instance; } }

		[SerializeField] BoosterData boosterData = null;
		RuntimeTimer timer = null;

		private void OnEnable ()
		{
			OnBoosterTakedListener.OnEnable ();
		}
		private void OnDisable ()
		{
			OnBoosterTakedListener.OnDisable ();
		}

		[ButtonGroup]
		public void ActivateOneLife () { InitBooster (boostersData["OneLife"]); }
		[ButtonGroup]
		public void ActivateBlade () { InitBooster (boostersData["Blade"]); }
		[ButtonGroup]
		public void ActivateShield () { InitBooster (boostersData["Shield"]); }

		public void OnColorInterractorDeath (UnityObjectVariable uov)
		{
			if (boosterData == null) return;
			if (!boosterData.Equals (boostersData["Blade"])) return;
			ColorBrick brick = null;
			if (!uov.CheckAs<ColorBrick> (out brick)) return;
			OnBrickDestroyed (brick);
		}
		private void OnBrickDestroyed (ColorBrick obj)
		{
			Vector3 startPosition = transform.position + transform.up * 0.5f;
			var dir = startPosition - obj.position;
			dir = GetDirection (obj.transform, dir);
			var distance = (2 * (dir.magnitude + 1f));
			dir.Normalize ();
			var ray = new Ray (startPosition, dir);
			var hit = new RaycastHit ();
			int CollectableLayer = LayerMask.NameToLayer ("Collectable");
			int CollectableMask = 1 << CollectableLayer;
			Vector3 end = startPosition + dir * distance;
			Debug.DrawLine (startPosition, end, Color.yellow, 5f);
			if (Physics.Raycast (ray, out hit, distance, CollectableMask))
			{
				ColorBrick colorBrick = hit.transform.GetComponent<ColorBrick> ();
				colorBrick.Death ();
			}
		}

		Vector3 GetDirection (Transform start, Vector3 dir)
		{
			Vector3 result = Vector3.zero;
			var leftAngle = Vector3.Angle (-start.right, dir);
			var rigthAngle = Vector3.Angle (start.right, dir);
			if (leftAngle < 90)
				result = -start.right;
			else if (rigthAngle < 90)
				result = start.right;

			return result;
		}

		public void OnBoosterTaked (UnityObjectVariable uov)
		{
			BoosterView boosterView = null;
			if (!uov.CheckAs (out boosterView)) return;
			BoosterData data = boosterView.data;
			if (!data) return;
			InitBooster (data);
		}

		void InitBooster (BoosterData data)
		{
			boosterData = data;
			timer = pool.Pop<RuntimeTimer> ("RuntimeTimer");
			timerView.Init (timer, data.Icon);

			data.InitTimer (timer);
			OnBoosterStart.Raise (data);
		}

	}
}
