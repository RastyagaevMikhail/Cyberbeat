using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class SkinTypeViewSettingsDataSelector : EnumDataSelector<SkinType, ViewSettings>
	{

	}

	[System.Serializable]
	public class ViewSettings
	{
		public Transform LookTarget;
		public Transform PositionTarget;
		public float RotationAngle;
		public float FogDensity;
		public float DurationMove = 1f;

	}
}
