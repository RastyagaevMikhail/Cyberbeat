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

	[MenuItem ("Game/Player/Input/Settings/Jump")]
	public static void OpenPlayerJumpInputSettings() {
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Data/InputControllers/InputSettings/JumpInputSettings.asset",typeof(Object));
	}
	[MenuItem ("Game/Player/Input/Settings/Center")]
	public static void OpenPlayerCenterInputSettings() {
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Data/InputControllers/InputSettings/CenterInputSettings.asset",typeof(Object));
	}
	[MenuItem ("Game/Player/Input/Settings/Side")]
	public static void OpenPlayerSideInputSettings() {
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Data/InputControllers/InputSettings/SideInputSettings.asset",typeof(Object));
	}
	[MenuItem ("Game/Player/Input/Controllers/JumpInputController")]
	public static void OpenPlayerJumpInputControllers() {
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Data/InputControllers/JumpInputController.asset",typeof(Object));
	}
	[MenuItem ("Game/Player/Input/Controllers/CenterInputController")]
	public static void OpenPlayerCenterInputControllers() {
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Data/InputControllers/CenterInputController.asset",typeof(Object));
	}
	[MenuItem ("Game/Player/Input/Controllers/SideInputController")]
	public static void OpenPlayerSideInputControllers() {
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Data/InputControllers/SideInputController.asset",typeof(Object));
	}
}
