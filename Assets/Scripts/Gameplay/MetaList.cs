using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using GameCore;
using Tools = GameCore.Tools;
using System;

using UnityEngine;

namespace CyberBeat
{
	[Serializable]
	public class MetaList
	{

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

		[ContextMenu ("PrintCount")]
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

	[Serializable]
	public class MetaDataListInfo
	{

		string NameOfGroup = "";
		[HideInInspector] public GameObject gameObject;

		public List<IMetaData> metaSettings = new List<IMetaData> ();

		public MetaDataListInfo (GameObject go, IMetaData newMeta)
		{
			gameObject = go;
			metaSettings = new List<IMetaData> ();
			metaSettings.Add (newMeta);
			NameOfGroup = go.name;
		}

		public bool Remove (IMetaData meta)
		{
			metaSettings.Remove (meta);
			Tools.Destroy (meta as UnityEngine.Object);
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
				Tools.Destroy (m as UnityEngine.Object);
			}
			metaSettings.Clear ();
		}
	}
}
