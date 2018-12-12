using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

	public class SpeedTimeUser : MetaDataUser<SpeedTimeMetaData>
	{
		private SplineController _splineController = null;
		public SplineController splineController { get { if (_splineController == null) _splineController = GetComponent<SplineController> (); return _splineController; } }

		[SerializeField] FloatVariable SpeedVariable;
		public override void OnMetaReached  (SpeedTimeMetaData meta)
		{
			SpeedVariable.DO (meta.Speed, meta.time);
		}

		public void _SetSpeed (float newSpeed)
		{
			Debug.Log ("_SetSpeed");
			splineController.Speed = newSpeed;
			splineController.Play ();
		}

	}
}
