using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace GameCore.Editor
{
	[OdinDrawer]
	public class DateTimeOdinDrawer : OdinValueDrawer<DateTime>
	{
		// protected override GUICallType GUICallType { get { return GUICallType.Rect; } }
		// protected override void DrawPropertyRect (Rect position, IPropertyValueEntry<DateTime> entry, GUIContent label)
		// {
		//     float widthRect = position.width / 4;
		//     var dayRect = new Rect(position) { width = widthRect };
		// }

		protected override void DrawPropertyLayout (IPropertyValueEntry<DateTime> entry, GUIContent label)
		{
			EditorGUILayout.PrefixLabel (label);
			DateTime value = entry.SmartValue;

			var Day = value.Day;
			var Month = value.Month;
			var Year = value.Year;

			var time = value.TimeOfDay;

			var Hours = time.Hours;
			var Minutes = time.Minutes;
			var Seconds = time.Seconds;
			SirenixEditorGUI.BeginBox ();
			EditorGUILayout.BeginVertical ();
			{
				EditorGUILayout.BeginHorizontal ();
				{
					Day = EditorGUILayout.IntField (Day);
					Month = EditorGUILayout.IntField (Month);
					Year = EditorGUILayout.IntField (Year);
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				{
					var labelWidth = EditorGUIUtility.labelWidth;
					EditorGUIUtility.labelWidth = EditorGUIUtility.fieldWidth / 4;
					EditorGUILayout.LabelField ("Day");
					EditorGUILayout.LabelField ("Month");
					EditorGUILayout.LabelField ("Year");
					EditorGUIUtility.labelWidth = labelWidth;
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				{
					Hours = EditorGUILayout.IntField (Hours);
					Minutes = EditorGUILayout.IntField (Minutes);
					Seconds = EditorGUILayout.IntField (Seconds);
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				{
					var labelWidth = EditorGUIUtility.labelWidth;
					EditorGUIUtility.labelWidth = EditorGUIUtility.fieldWidth / 4;
					EditorGUILayout.LabelField ("Hours");
					EditorGUILayout.LabelField ("Minutes");
					EditorGUILayout.LabelField ("Seconds");
					EditorGUIUtility.labelWidth = labelWidth;
				}
				EditorGUILayout.EndHorizontal ();

				// EditorGUILayout.BeginHorizontal ();
				// {
				// 	Days = Change (Days);
				// 	Hours = Change (Hours);
				// 	Minutes = Change (Minutes);
				// 	Seconds = Change (Seconds);

				// }
				// EditorGUILayout.EndHorizontal ();
			}
			EditorGUILayout.EndVertical ();
			SirenixEditorGUI.EndBox ();
			entry.SmartValue = new DateTime (Year, Month, Day, Hours, Minutes, Seconds);
			entry.ApplyChanges ();
		}

		int Change (int value)
		{
			EditorGUILayout.BeginHorizontal ();
			{
				if (GUILayout.Button ("+")) value++;
				if (GUILayout.Button ("-")) value--;
			}
			EditorGUILayout.EndHorizontal ();
			return value;
		}
	}
}
