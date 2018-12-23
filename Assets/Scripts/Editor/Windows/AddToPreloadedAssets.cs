using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore.Editor
{
	using GameCore;

	using UnityEditor;
#if UNITY_2018_2_OR_NEWER
	public class AddToPreloadedAssets : OdinEditorWindow
	{
		[MenuItem ("Game/Windows/AddToPreloadedAssets")]
		private static void OpenWindow ()
		{
			GetWindow<AddToPreloadedAssets> ().Show ();
		}

		[SerializeField] List<ScriptableObject> scriptableObjects;

		[Button] public void Load () { scriptableObjects = Resources.LoadAll<ScriptableObject> ("Data").ToList ().FindAll (isd => isd is ISingletonData).ToList (); }

		[Button] public void AddToPReloaded ()
		{
			PlayerSettings.SetPreloadedAssets (scriptableObjects.ToArray ());
		}
	}
#endif
}
