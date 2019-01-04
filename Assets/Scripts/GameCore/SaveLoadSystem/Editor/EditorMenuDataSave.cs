using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore.Editor
{
	using GameCore;
    using System;
    using System.Linq;
	public class EditorMenuDataSave
	{

		[UnityEditor.MenuItem ("Game/Reset/Default")]
		public static void ResetAllDefault ()
		{
			IEnumerable<ISingletonData> enumerable = Resources.LoadAll<ScriptableObject> ("Data").ToList ()
				.FindAll (so => so is ISingletonData)
				.Select (so => so as ISingletonData);

			foreach (var data in enumerable)
				data.ResetDefault ();

			Debug.Log (Tools.LogCollection (enumerable));
		}

		[UnityEditor.MenuItem ("Game/Reset/PlayerPrefs")]
		public static void ResetPlayerPrefs ()
		{
			PlayerPrefs.DeleteAll ();
		}
[UnityEditor.MenuItem ("Game/Actions/Add to Preloaded Assets")]
		public static void AddToPreloadedAssets ()
		{
			// var PreoloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets ().ToList ();

			// PreoloadedAssets.Clear ();

			// IEnumerable<ScriptableObject> Singletons = Resources.LoadAll<ScriptableObject> ("Data")
			// 	.ToList ()
			// 	.FindAll (so => so is ISingletonData);

			// foreach (var singleton in Singletons)
			// 	PreoloadedAssets.Add (singleton);

			// // UnityEditor.PlayerSettings.SetPreloadedAssets (PreoloadedAssets.ToArray ());
			UnityEditor.PlayerSettings.SetPreloadedAssets (Array.FindAll (Resources.LoadAll<ScriptableObject> ("Data"), so => so is ISingletonData));
		}
	}
}
