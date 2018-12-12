#if UNITY_EDITOR
using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using Tools = GameCore.Tools;
namespace CyberBeat
{

	[Serializable]
	public class TimePointsGizmoDrawer
	{
		[SerializeField] bool showGizmo = true;
		[SerializeField]
		Color Start_Color = Color.green;
		[SerializeField]
		Color End_Color = Color.red;

		[SerializeField, Range (0, 1)]
		float sizePoint = 0.2f;
		[SerializeField]
		List<TimePoints> timePoints = new List<TimePoints> ();
		public TimePointsGizmoDrawer (List<TimePoints> _timePoints)
		{
			timePoints = _timePoints;
		}
		public void Draw ()
		{
			if (showGizmo)
			{
				foreach (var point in timePoints)
				{
					DrawTimePointGizmo (Start_Color, point.Start);
					DrawTimePointGizmo (End_Color, point.End);
				}
			}
		}
		private void DrawTimePointGizmo (Color color, TimePointInfo info)
		{
			Gizmos.color = color;
			float size = HandleUtility.GetHandleSize (info.position);
			Handles.Label (info.position + info.Up * size, info.MetaData, Tools.BackdropHtmlLabel);
			Gizmos.DrawSphere (info.position, size * sizePoint);
		}
	}
}
#endif
