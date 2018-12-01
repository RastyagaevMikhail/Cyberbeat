using System.Collections.Generic;

namespace CyberBeat
{
	using Sirenix.OdinInspector;
	using Sirenix.Serialization;

	using UnityEngine;

	[CreateAssetMenu (fileName = "TimePointsData", menuName = "Color Bricks/TimePointsData", order = 0)]
	public class TimePointsData : SerializedScriptableObject
	{
		[SerializeField]
		public List<TimePoints> points = new List<TimePoints> ();
	}
}
