using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore.Editor
{
	using GameCore;

	using System.Linq;
	using System;

	using UnityEditor;
	using Tools = Tools;

	public class EditorMenuDataSave
	{
		static SaveData _saveData;
		static SaveData saveData => _saveData??(_saveData = Tools.GetAssetAtPath<SaveData> ("Assets/Resources/Data/SaveData.asset"));
		[MenuItem ("Game/Reset/All", priority = 0)]
		public static void ResetAll ()
		{
			ResetPlayerPrefs ();
			ResetAllDefault ();
		}

		[MenuItem ("Game/Reset/Default/All", priority = 1)]
		public static void ResetAllDefault ()
		{
			IEnumerable<IResetable> enumerable = Tools.GetAtPath<ScriptableObject> ("Assets").ToList ()
				.FindAll (so => so is IResetable)
				.Select (so => so as IResetable);

			foreach (var data in enumerable)
				data.ResetDefault ();

			Debug.Log (Tools.LogCollection (enumerable));
		}

		[MenuItem ("Game/Reset/Default/Varaiables", priority = 2)]
		private static void ResetDeafultVariables ()
		{
			List<ASavableVariable> variables = Tools.GetAtPath<ASavableVariable> ("Assets").ToList ();
			foreach (var variable in variables)
			variable.ResetDefault ();
			Debug.Log (Tools.LogCollection (variables));
		}

		[MenuItem ("Game/Reset/Default/Singletons", priority = 3)]
		private static void ResetDefaultSingletons ()
		{
			IEnumerable<ISingletonData> enumerable = Resources.LoadAll<ScriptableObject> ("Data").ToList ()
				.FindAll (so => so is ISingletonData)
				.Select (so => so as ISingletonData);

			foreach (var data in enumerable)
				data.ResetDefault ();

			Debug.Log (Tools.LogCollection (enumerable));
		}

		[MenuItem ("Game/Reset/PlayerPrefs", priority = 4)]
		public static void ResetPlayerPrefs ()
		{
			PlayerPrefs.DeleteAll ();
		}

		[MenuItem ("Game/Actions/Add to Preloaded Assets")]
		public static void AddToPreloadedAssets ()
		{
			PlayerSettings.SetPreloadedAssets (Array.FindAll (Resources.LoadAll<ScriptableObject> ("Data"), so => so is ISingletonData));
		}

		[InitializeOnLoadMethod]
		static void SubscrideOneditorChangePlayingSatate ()
		{
			UnityEditor.EditorApplication.playModeStateChanged -= playmodeStateChanged;
			UnityEditor.EditorApplication.playModeStateChanged += playmodeStateChanged;
		}

		private static void playmodeStateChanged (UnityEditor.PlayModeStateChange state)
		{
			if (!UnityEditor.EditorApplication.isPlaying)
			{
				saveData.ResetLoaded ();
			}
		}
	}
}
