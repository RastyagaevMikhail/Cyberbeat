using System;
using GameCore;
using UnityEngine;

namespace CyberBeat
{
	[Serializable]
	public class ViewSettings
	{
		public Transform LookTarget => lookTargetVariable.ValueFast;
		[SerializeField] TransformVariable lookTargetVariable;
		public Transform PositionTarget => PositionTargetVariable.ValueFast;
		[SerializeField] TransformVariable PositionTargetVariable;
		public float RotationAngle;
		public float FogDensity;
		public float DurationMove = 1f;

	}
}
