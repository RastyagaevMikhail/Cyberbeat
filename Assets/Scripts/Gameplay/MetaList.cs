using Sirenix.OdinInspector;
using Sirenix.Utilities;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

using UnityEditor;
#endif

using GameCore;
using Tools = GameCore.Tools;
using UnityEngine;
namespace CyberBeat
{
	[System.Serializable]
	public class MetaList
	{
		[ListDrawerSettings (
				OnBeginListElementGUI = "OnBeginListElementGUI",
				OnEndListElementGUI = "OnEndListElementGUI",
				IsReadOnly = true,
				Expanded = true,
				HideAddButton = true),
			ShowInInspector, /* InlineProperty (LabelWidth = 10), */ HideLabel, /* InlineEditor */
		]
		// [FoldoutGroup ("$MetaDataListInfo.NameOfGroup")]
		[SerializeField]
		List<MetaDataListInfo> metaList = new List<MetaDataListInfo> ();

		public void Add (IMetaData meta)
		{
			MonoBehaviour monoBehaviour = (meta as MonoBehaviour);
			// Debug.LogFormat (monoBehaviour, "monoBehaviour = {0}", monoBehaviour);
			var go = monoBehaviour.gameObject;
			var metaGO = metaList.Find (m => m.gameObject.Equals (go));
			if (metaGO != null)
			{
				metaGO.Add (meta);
			}
			else
			{
				metaGO = new MetaDataListInfo (go, meta);
				metaList.Add (metaGO);
			}
			SortByName ();
		}

		public void SortByName ()
		{
			metaList.Sort ((a, b) => a.gameObject.name.CompareTo (b.gameObject.name));
		}

		[Button ("PrintCount")]
		void PrintCount ()
		{
			Debug.LogFormat ("meta.Count = {0}", metaList.Count);
		}
		public void Remove (IMetaData meta)
		{
			MonoBehaviour monoBehaviour = (meta as MonoBehaviour);
#if UNITY_EDITOR
			UnityEditor.Undo.RegisterCompleteObjectUndo (monoBehaviour, monoBehaviour.name);
			Debug.LogFormat ("on Remove {0}", monoBehaviour);
#endif
			var go = monoBehaviour.gameObject;
			var metaGO = metaList.Find (m => m.gameObject.Equals (go));
			if (metaGO != null)
			{
				bool isLastDeleted = metaGO.Remove (meta);
				if (isLastDeleted)
					metaList.Remove (metaGO);
			}
			SortByName ();
		}
#if UNITY_EDITOR
		void OnBeginListElementGUI (int i)
		{
			EditorGUILayout.BeginHorizontal ();
			MetaDataListInfo metaDataListInfo = metaList[i];
		}
		void OnEndListElementGUI (int i)
		{
			if (i.InRange (0, metaList.Count - 1))
				DrawRemoveButton (metaList[i]);
			EditorGUILayout.EndHorizontal ();
		}

		private void DrawRemoveButton (MetaDataListInfo metaData)
		{
			float rectSize = 20;
			GUILayoutOption width = GUILayout.Width (rectSize);
			GUILayoutOption height = GUILayout.Height (rectSize);
			if (GUILayout.Button (EditorIcons.X.Active, width, height))
			{
				metaData.Clear ();

				metaList.Remove (metaData);
			}
		}
		public void OnValidate (GameObject gameObject)
		{
			if (metaList == null) metaList = new List<MetaDataListInfo> ();
			IMetaData[] metaDatas = gameObject.GetComponents<IMetaData> ();
			foreach (var metaData in metaDatas)
				if (!metaList.Exists (mdli =>
					{
						if (mdli.metaSettings == null) mdli.metaSettings = new List<IMetaData> ();
						return mdli.metaSettings.Contains (metaData);
					}))
					Add (metaData);
		}

#endif
		public void Clear ()
		{
			foreach (var m in metaList)
			{
				m.Clear ();
			}
			metaList.Clear ();
		}

	}

	[System.Serializable]
	public class MetaDataListInfo
	{

		string NameOfGroup = "";
		[HideInInspector] public GameObject gameObject;
		// [FoldoutGroup ("$GetMetaTypeName")]
		[
			// InlineEditor (IncrementInlineEditorDrawerDepth = true),
			//   InlineProperty (LabelWidth = 10) ,
			HideLabel,
			ListDrawerSettings (
				OnBeginListElementGUI = "OnBeginListElementGUI",
				OnEndListElementGUI = "OnEndListElementGUI",
				IsReadOnly = true,
				Expanded = true,
				HideAddButton = true)
		]
		public List<IMetaData> metaSettings = new List<IMetaData> ();

		public MetaDataListInfo (GameObject go, IMetaData newMeta)
		{
			gameObject = go;
			metaSettings = new List<IMetaData> ();
			metaSettings.Add (newMeta);
			NameOfGroup = go.name;
		}
		// string GetMetaTypeName (int index)
		// {
		// 	return metaSettings[index].NameOfMetaType;
		// }
		// static string GetMetaTypeName (IMetaData meta)
		// {
		// 	return meta.NameOfMetaType;
		// }
#if UNITY_EDITOR

		void OnBeginListElementGUI (int i)
		{
			EditorGUILayout.BeginHorizontal ();
			IMetaData metaData = metaSettings[i];
			// var context = new Sirenix.Utilities.Editor.GUIContext<IMetaData> () { Value = metaData };
			Sirenix.Utilities.Editor.SirenixEditorFields.PolymorphicObjectField (metaData, metaData.GetType (), true);

		}
		void OnEndListElementGUI (int i)
		{
			var metaData = metaSettings[i];
			DrawRemoveButton (metaData);
			EditorGUILayout.EndHorizontal ();
		}
		private void DrawRemoveButton (IMetaData metaData)
		{
			float rectSize = 20;
			GUILayoutOption width = GUILayout.Width (rectSize);
			GUILayoutOption height = GUILayout.Height (rectSize);
			if (GUILayout.Button (EditorIcons.X.Active, width, height)) Remove (metaData);
		}
#endif
		public bool Remove (IMetaData meta)
		{
			metaSettings.Remove (meta);
			Tools.Destroy (meta as Object);
			return metaSettings.Count == 0;
		}

		public void Add (IMetaData meta)
		{
			metaSettings.Add (meta);
		}
		public bool Contains (IMetaData metaData)
		{
			return metaSettings.Contains (metaData);
		}
		public void Clear ()
		{
			if (metaSettings == null) return;
			foreach (var m in metaSettings.ToArray ())
			{
				Tools.Destroy (m as Object);
			}
			metaSettings.Clear ();
		}
	}
#if UNITY_EDITOR

	[OdinDrawer]
	public class StatListValueDrawer : OdinValueDrawer<MetaList>
	{
		protected override void DrawPropertyLayout (IPropertyValueEntry<MetaList> entry, GUIContent label)
		{
			// This would be the "private List<MetaList> stats" field.
			entry.Property.Children[0].Draw (label);
			entry.Property.Children[1].Draw (label);
		}
	}

#endif
}
