using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevToolsEditor;

using GameCore;

using Sirenix.OdinInspector.Editor;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using Tools = GameCore.Tools;

using UnityEngine;
namespace CyberBeat
{
	[CustomEditor (typeof (SpeedTimeMetaData))]
	public class SpeedTimeMetaDataEditor : OdinEditor
	{

		static MetaDataOptions.SpeedMetaDataOption speedMetaData { get { return MetaDataOptions.speedMetaData; } }

		[DrawGizmo (GizmoType.Active | GizmoType.NonSelected | GizmoType.InSelectionHierarchy)]
		static void MetaGizmoDrawer (SpeedTimeMetaData data, GizmoType context)
		{
			if (CurvyGlobalManager.ShowMetadataGizmo && speedMetaData.enabled)
			{
				CurvySplineSegment controlPoint = data.ControlPoint;
				Vector3 p = controlPoint.position;
				Color ColorOne = speedMetaData.GradienSpeedColorOne;
				Handles.color = ColorOne;
				Handles.DrawLine (p, p + data.Up);
				p += data.Up * HandleUtility.GetHandleSize (p);
				string Text = string.Format (Tools.LogTextInColor (@"{0}", ColorOne), data);
				DrawLabeleWithOutLine (Text, p);
				// Handles.Label (p, Text, DTStyles.BackdropHtmlLabel);
				if (speedMetaData.ShowPoints)
				{

					Vector3 endPoint = default (Vector3);
					List<EditorSpeedData> lableData = RefreshSpeedData (data, ref endPoint);
					foreach (var d in lableData)
						DrawSpeedLable (d);

					Handles.SphereHandleCap (0, endPoint, Quaternion.identity, HandleUtility.GetHandleSize (endPoint) * 0.2f, EventType.Repaint);
				}

			}
		}

		private static List<EditorSpeedData> RefreshSpeedData (SpeedTimeMetaData data, ref Vector3 endPoint)
		{
			CurvySplineSegment controlPoint = data.ControlPoint;
			var editorSpeedDataList = new List<EditorSpeedData> ();

			int Count = MetaDataOptions.speedMetaData.CountPoints;
			float deltaF = data.FPoint / Count;
			Color ColorOne = speedMetaData.GradienSpeedColorOne;
			Color ColorTwo = speedMetaData.GradienSpeedColorTwo;
			var gradient = GetGradientByTwoColors (ColorOne, ColorTwo);
			for (int i = 0; i < Count; i++)
			{
				EditorSpeedData eData = new EditorSpeedData ();
				int NextIndex = i + 1;
				float CurrebtLocalF = deltaF * NextIndex;
				eData.point = controlPoint.Interpolate (CurrebtLocalF);
				eData.speed = data.InterpolateSpeedByPosition (eData.point);
				eData.up = controlPoint.GetOrientationUpFast (CurrebtLocalF);
				eData.color = gradient.Evaluate ((float) NextIndex / (float) Count);
				if (NextIndex == Count)
					endPoint = eData.point;

				editorSpeedDataList.Add (eData);
			}
			return editorSpeedDataList;
		}

		private static void DrawSpeedLable (EditorSpeedData data)
		{
			DrawSpeedLable (data.point, data.up, data.speed, data.color);
		}
		private static void DrawSpeedLable (Vector3 point, Vector3 up, float speed, Color color)
		{
			string Text;
			var vPos = point + up * HandleUtility.GetHandleSize (point) * .5f;
			Handles.color = color;
			Handles.DrawLine (point, vPos - up * 0.5f);

			string format = Tools.LogTextInColor (@"{0:f1} {1}", color);

			string positionStr = MetaDataOptions.speedMetaData.ShowPossition ? point.ToString () : "";

			Text = string.Format (format, speed, positionStr);

			DrawLabeleWithOutLine (Text, vPos);
		}

		private static void DrawLabeleWithOutLine (string Text, Vector3 pos)
		{
			GUIStyle style = new GUIStyle (DTStyles.BackdropHtmlLabel);
			Handles.Label (pos, Text, DTStyles.BackdropHtmlLabel);
			Handles.color = Color.black;
			Handles.color = Color.black;
			style.normal.background = null;
			style.normal.textColor = Color.black;
			style.fontSize += 2 + speedMetaData.OutlineSize;
			Handles.Label (pos, Text, style);
		}

		static Gradient GetGradientByTwoColors (Color one, Color two)
		{
			Gradient g;
			GradientColorKey[] gck;
			GradientAlphaKey[] gak;
			g = new Gradient ();
			gck = new GradientColorKey[2];
			gck[0].color = one;
			gck[0].time = 0.0F;
			gck[1].color = two;
			gck[1].time = 1.0F;
			gak = new GradientAlphaKey[2];
			gak[0].alpha = 1.0F;
			gak[0].time = 0.0F;
			gak[1].alpha = 1.0F;
			gak[1].time = 1.0F;
			g.SetKeys (gck, gak);
			return g;
		}
	}
}
