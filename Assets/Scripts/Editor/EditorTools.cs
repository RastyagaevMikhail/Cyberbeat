using GameCore;

using SonicBloom.Koreo;
using SonicBloom.Koreo.EditorUI;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
namespace GameCore
{

	public class EditorTools
	{
		[MenuItem ("GameObject/Swap whith parent", false, 0)]
		static void SwapWhithParent (MenuCommand mc)
		{
			var go = Selection.activeGameObject;
			Transform parent = go.transform.parent;
			go.transform.parent = null;
			parent.SetParent (go.transform);
		}

		[MenuItem ("GameObject/Empty", false, 0)]
		static void Empty (MenuCommand mc)
		{
			var newGOtransform = new GameObject ().transform;
			if (Selection.gameObjects.Length > 1)
			{

				foreach (var go in Selection.gameObjects)
				{
					go.transform.SetParent (newGOtransform);
				}
				newGOtransform.CenterOnChildred ();
			}
			else
			{
				var go = Selection.activeGameObject;
				newGOtransform.SetParent (go.transform);
			}

		}
		public static void CreateAsset (UnityEngine.Object asset, string path)
		{
			string[] splitedPath = path.Split ('/');
			string validatedPath = splitedPath[0];

			List<string> list = splitedPath.ToList ();
			list.RemoveAt (0);
			splitedPath = list.ToArray ();

			foreach (var subPath in splitedPath)
			{
				if (!AssetDatabase.IsValidFolder (validatedPath + "/" + subPath))
				{
					AssetDatabase.CreateFolder (validatedPath, subPath);
				}
				validatedPath += "/" + subPath;
			}
			EditorUtility.SetDirty (asset);
			AssetDatabase.CreateAsset (asset, path);
			AssetDatabase.SaveAssets ();
		}

	}
}
