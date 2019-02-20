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

		[MenuItem ("Game/Reset/Default/All")]
		public static void ResetAllDefault ()
		{
			IEnumerable<IResetable> enumerable = Tools.GetAtPath<ScriptableObject> ("Assets").ToList ()
				.FindAll (so => so is IResetable)
				.Select (so => so as IResetable);

			foreach (var data in enumerable)
				data.ResetDefault ();

			Debug.Log (Tools.LogCollection (enumerable));
		}

		[MenuItem ("Game/Reset/Default/Singletons")]
		private static void ResetDefaultSingletons ()
		{
			IEnumerable<ISingletonData> enumerable = Resources.LoadAll<ScriptableObject> ("Data").ToList ()
			.FindAll (so => so is ISingletonData)
			.Select (so => so as ISingletonData);

			foreach (var data in enumerable)
			data.ResetDefault ();

			Debug.Log (Tools.LogCollection (enumerable));
		}

		[MenuItem ("Game/Reset/Default/Varaiables")]
		private static void ResetDeafultVariables ()
		{
			List<ASavableVariable> variables = Tools.GetAtPath<ASavableVariable> ("Assets").ToList ();
			foreach (var variable in variables)
				variable.ResetDefault ();
			Debug.Log (Tools.LogCollection (variables));
		}

		[MenuItem ("Game/Reset/PlayerPrefs")]
		public static void ResetPlayerPrefs ()
		{
			PlayerPrefs.DeleteAll ();
		}

		[MenuItem ("Game/Actions/Add to Preloaded Assets")]
		public static void AddToPreloadedAssets ()
		{
			PlayerSettings.SetPreloadedAssets (Array.FindAll (Resources.LoadAll<ScriptableObject> ("Data"), so => so is ISingletonData));
		}
	}
}
