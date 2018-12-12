using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;

using UnityEditor;

#endif

namespace CyberBeat
{
	[System.Serializable]
	public class BitInfo
	{
		public float time;
		[HideInInspector]
		public int[] presets;
#if UNITY_EDITOR

		[OnInspectorGUI]
		private void OnInspectorGUI ()
		{
			if (presets == null) return;

			SirenixEditorGUI.BeginIndentedHorizontal ();
			EditorGUILayout.PrefixLabel (new GUIContent ("Presets"));
			for (int i = 0; i < presets.Length; i++)
			{
				presets[i] = SirenixEditorFields.IntField (presets[i]);
			}
			SirenixEditorGUI.EndIndentedHorizontal ();

		}

		private static int DrawPresetElemnt (Rect rect, int value)
		{
			Debug.LogFormat ("rect = {0}", rect);
			return SirenixEditorFields.IntField (rect, value);
		}
#endif
	}
}
