using System;
using GameCore;
using UnityEngine;

namespace CyberBeat
{
	[Serializable]
	public class ViewSettings
	{
		public Transform LookTarget => lookTargetVariable.Value;
		[SerializeField] TransformVariable lookTargetVariable;
		public Transform PositionTarget => PositionTargetVariable.Value;
		[SerializeField] TransformVariable PositionTargetVariable;
		public float RotationAngle;
		public float FogDensity;
		public float DurationMove = 1f;

	}
}
