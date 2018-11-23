#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace GameCore.Editor
{
	using Editor =  UnityEditor.Editor;
	using GameCore;
	[CustomEditor (typeof (LocalizeTextMeshProUGUI))]
	public class LocalizeTextMeshProUGUIEditor : Editor
	{
		private readonly GUILayoutOption[] defaultGUI = new GUILayoutOption[0];
		bool onFix = false;
		string newID;
		List<Translation> translationFromAdd = new List<Translation> ();
		private bool notFixed = true;

		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();

			LocalizeTextMeshProUGUI locText = (LocalizeTextMeshProUGUI) target;
			string[] values = Localizator.GetValuesForKey (locText.Id);
			EditorGUILayout.Separator ();
			if (values.Length > 0)
			{
				if (GUILayout.Button (onFix ? "End Fix" : "Fix ID")) onFix = !onFix;
				if (onFix)
				{
					if (notFixed)
					{
						newID = locText.Id;
						notFixed = false;
					}
					newID = EditorGUILayout.TextField ("ID", newID);

				}
				else
				{
					if (notFixed == false && newID != locText.Id)
					{
						Localizator.AddTranslation (newID);
						for (int i = 0; i < values.Length; i++)
						{
							Localizator.SaveLocalization (newID, Localizator.Languages[i], values[i]);
						}
						Localizator.RemoveTranslation (locText.Id);
						locText.Id = newID;
					}
					notFixed = true;
				}
				EditorGUILayout.Separator ();

				for (int i = 0; i < values.Length; i++)
				{
					GUILayout.BeginHorizontal (defaultGUI);
					EditorGUILayout.LabelField (Localizator.Languages[i].ToString (), defaultGUI);

					var newText = EditorGUILayout.TextField (values[i], defaultGUI);
					if (newText.Equals (values[i]) == false)
					{
						Localizator.SaveLocalization (locText.Id, Localizator.Languages[i], newText);
						EditorUtility.SetDirty (target);
					}

					GUILayout.EndHorizontal ();
				}
			}
			else
			{
				GUILayout.BeginHorizontal (defaultGUI);
				bool addTrans = GUILayout.Button (new GUIContent ("Add Translation"), defaultGUI);
				bool addDefaultTrans = GUILayout.Button (new GUIContent ("Add Default Translations"), defaultGUI);
				GUILayout.EndHorizontal ();
				if (addTrans)
				{
					if (translationFromAdd == null) translationFromAdd = new List<Translation> ();
					translationFromAdd.Add (new Translation () { language = Localizator.GetLanguage () });
				}
				if (addDefaultTrans)
				{
					if (translationFromAdd == null) translationFromAdd = new List<Translation> ();
					foreach (var lang in Localizator.Languages)
						translationFromAdd.Add (new Translation () { language = lang });
				}
				foreach (var trans in translationFromAdd)
				{
					GUILayout.BeginHorizontal (defaultGUI);
					var langstr = EditorGUILayout.EnumPopup (trans.language, defaultGUI).ToString ();
					trans.language = (SystemLanguage) Enum.Parse (typeof (SystemLanguage), langstr);

					trans.value = EditorGUILayout.TextField (trans.value, defaultGUI);
					GUILayout.EndHorizontal ();
				}
				if (translationFromAdd != null && translationFromAdd.Count != 0)
				{
					bool SaveTrans = GUILayout.Button (new GUIContent ("Save Translations"), defaultGUI);
					if (SaveTrans)
					{
						Localizator.AddTranslation (locText.Id);
						foreach (var trans in translationFromAdd)
						{
							Localizator.SaveLocalization (locText.Id, trans.language, trans.value);
							EditorUtility.SetDirty (target);
						}
						translationFromAdd.Clear ();
					}
				}

			}

		}
	}
	public class Translation
	{
		public SystemLanguage language;
		public string value;
	}
}
#endif
