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
		EditorSceneManager.OpenScene (path);
	}

	[MenuItem ("Game/Scenes/Loading")]
	public static void OpenLoading ()
	{
		OpenScene ("Loading");
	}

	[MenuItem ("Game/Scenes/Menu")]
	public static void OpenMenu ()
	{
		OpenScene ("Menu");
	}
	[MenuItem ("Game/Scenes/InitializeScene")]
	public static void OpenInitializeScene ()
	{
		OpenScene ("InitializeScene");
	}
	[MenuItem ("Game/Scenes/Tutorial")]
	public static void OpenTutorial ()
	{
		OpenScene ("Tutorial");
	}

	[MenuItem ("Game/Scenes/Tracks/E.P.O - Infinity")]
	public static void OpenEPO_Infinity ()
	{
		OpenScene ("Tracks/E.P.O - Infinity");
	}
	[MenuItem ("Game/Scenes/Tracks/Mountkid - Dino")]
	public static void OpenMountkid_Dino ()
	{
		OpenScene ("Tracks/Mountkid - Dino");
	}
}
