#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
namespace GameCore.Editor
{
	using Editor = UnityEditor.Editor;
	using GameCore;

	using Sirenix.OdinInspector.Editor;
	using Sirenix.Utilities.Editor;

	[CustomEditor (typeof (LocalizeTextMeshProUGUI))]
	public class LocalizeTextMeshProUGUIEditor : OdinEditor
	{
		public Localizator localizator { get { return Localizator.instance; } }
		private readonly GUILayoutOption[] defaultGUI = new GUILayoutOption[0];
		bool onFix = false;
		string newID;
		List<Translation> translationFromAdd = new List<Translation> ();
		private bool notFixed = true;
		private bool notAdded = true;
		private bool onNew;

		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();

			LocalizeTextMeshProUGUI locText = (LocalizeTextMeshProUGUI) target;
			string[] values = localizator.GetValuesForKey (locText.Id);
			EditorGUILayout.Separator ();
			if (values.Length > 0)
			{
				EditorGUILayout.BeginHorizontal ();
				Fix (locText, values);
				New (locText, values);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Separator ();

				for (int i = 0; i < values.Length; i++)
				{
					GUILayout.BeginHorizontal (defaultGUI);
					EditorGUILayout.LabelField (localizator.Languages[i].ToString (), defaultGUI);

					var newText = EditorGUILayout.TextField (values[i], defaultGUI);
					if (newText.Equals (values[i]) == false)
					{
						localizator.SaveLocalization (locText.Id, localizator.Languages[i], newText);
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
					translationFromAdd.Add (new Translation () { language = localizator.GetLanguage () });
				}
				if (addDefaultTrans)
				{
					if (translationFromAdd == null) translationFromAdd = new List<Translation> ();
					foreach (var lang in localizator.Languages)
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
						localizator.AddTranslation (locText.Id);
						foreach (var trans in translationFromAdd)
						{
							localizator.SaveLocalization (locText.Id, trans.language, trans.value);
							EditorUtility.SetDirty (target);
						}
						translationFromAdd.Clear ();
					}
				}

			}

		}

		private void New (LocalizeTextMeshProUGUI locText, string[] values)
		{
			EditorGUILayout.BeginVertical ();
			if (GUILayout.Button (onNew ? "Save" : "New ID")) onNew = !onNew;
			if (onNew)
			{
				if (notAdded)
				{
					newID = locText.Id;
					notAdded = false;
					translationFromAdd = new List<Translation> ();
					foreach (var lang in localizator.Languages)
						translationFromAdd.Add (new Translation () { language = lang });
				}
				newID = EditorGUILayout.TextField ("ID", newID);
				foreach (var trans in translationFromAdd)
				{
					GUILayout.BeginHorizontal (defaultGUI);
					var langstr = EditorGUILayout.EnumPopup (trans.language, defaultGUI).ToString ();
					trans.language = (SystemLanguage) Enum.Parse (typeof (SystemLanguage), langstr);

					trans.value = EditorGUILayout.TextField (trans.value, defaultGUI);
					GUILayout.EndHorizontal ();
				}
			}
			else
			{
				if (notAdded == false && newID != locText.Id)
				{
					localizator.AddTranslation (newID);
					for (int i = 0; i < values.Length; i++)
					{
						localizator.SaveLocalization (newID, localizator.Languages[i], values[i]);
					}
					locText.Id = newID;
					localizator.AddTranslation (locText.Id);
					foreach (var trans in translationFromAdd)
					{
						localizator.SaveLocalization (locText.Id, trans.language, trans.value);
						EditorUtility.SetDirty (target);
					}
					translationFromAdd.Clear ();
				}
				notAdded = true;
			}
			EditorGUILayout.EndVertical ();
		}

		private void Fix (LocalizeTextMeshProUGUI locText, string[] values)
		{
			EditorGUILayout.BeginVertical ();
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
					localizator.AddTranslation (newID);
					for (int i = 0; i < values.Length; i++)
					{
						localizator.SaveLocalization (newID, localizator.Languages[i], values[i]);
					}
					localizator.RemoveTranslation (locText.Id);
					locText.Id = newID;
				}
				notFixed = true;
			}
			EditorGUILayout.EndVertical ();
		}
	}
	public class Translation
	{
		public SystemLanguage language;
		public string value;
	}
}
#endif
