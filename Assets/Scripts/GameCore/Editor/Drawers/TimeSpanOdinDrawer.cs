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
	public class TimeSpanOdinDrawer : OdinValueDrawer<TimeSpan>
	{
		// protected override GUICallType GUICallType { get { return GUICallType.Rect; } }
		// protected override void DrawPropertyRect (Rect position, IPropertyValueEntry<TimeSpan> entry, GUIContent label)
		// {
		//     float widthRect = position.width / 4;
		//     var dayRect = new Rect(position) { width = widthRect };
		// }

		protected override void DrawPropertyLayout (IPropertyValueEntry<TimeSpan> entry, GUIContent label)
		{
			EditorGUILayout.PrefixLabel(label);
			TimeSpan value = entry.SmartValue;
			var Days = value.Days;
			var Hours = value.Hours;
			var Minutes = value.Minutes;
			var Seconds = value.Seconds;
			SirenixEditorGUI.BeginBox ();
			EditorGUILayout.BeginVertical ();
			{
				EditorGUILayout.BeginHorizontal ();
				{
					Days = EditorGUILayout.IntField (Days);
					Hours = EditorGUILayout.IntField (Hours);
					Minutes = EditorGUILayout.IntField (Minutes);
					Seconds = EditorGUILayout.IntField (Seconds);
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				{
					var labelWidth = EditorGUIUtility.labelWidth;
					EditorGUIUtility.labelWidth = EditorGUIUtility.fieldWidth / 5;
					EditorGUILayout.LabelField ("Days");
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
			entry.SmartValue = new TimeSpan (Days, Hours, Minutes, Seconds);
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
