using CyberBeat;

using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;

public class EditorMenuTools
{

	public static void OpenScene (string SceneName)
	{
		string path = string.Format ("Assets/Scenes/{0}.unity", SceneName);
		if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ())
			EditorSceneManager.OpenScene (path);
	}
	[MenuItem ("Game/Scenes/InitializeScene", false, 0)]
	public static void OpenInitializeScene ()
	{
		OpenScene ("InitializeScene");
	}
	[MenuItem ("Game/Scenes/Loading", false, 1)]
	public static void OpenLoading ()
	{
		OpenScene ("Loading");
	}
	[MenuItem ("Game/Scenes/Tutorial", false, 2)]
	public static void OpenTutorial ()
	{
		OpenScene ("Tutorial");
	}
	[MenuItem ("Game/Scenes/Menu", false, 3)]
	public static void OpenMenu ()
	{
		OpenScene ("Menu");
	}
	[MenuItem ("Game/Scenes/Track", false, 4)]
	public static void OpenTrack ()
	{
		OpenScene ("Track");
	}
	[MenuItem ("Game/Current Track")]
	public static void OpenCurrent ()
	{
		Selection.activeObject = GameCore.Tools.GetAssetAtPath<TrackVariable> ("Assets/Data/Variables/Track/CurrentTrack.asset").Value;
	}
}
