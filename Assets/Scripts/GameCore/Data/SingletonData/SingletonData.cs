using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GameCore
{
	public abstract class SingletonData<T> : ScriptableObject, ISingletonData where T : SingletonData<T>
	{
		#region  instance
#if UNITY_EDITOR
		//[UnityEditor.MenuItem ("Game/Data/T")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		[ContextMenu ("Init On Create")] public abstract void InitOnCreate ();
		[ContextMenu ("Reset Default")] public abstract void ResetDefault ();
#else
		public abstract void ResetDefault ();
#endif

		static string typeName { get { return typeof (T).Name; } }
		private static T _instance;
		public static T instance
		{
			get
			{
				if (_instance == null) _instance = Resources.Load<T> ("Data/{0}".AsFormat (typeName));
#if UNITY_EDITOR
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<T> ();
					try { _instance.InitOnCreate (); }
					catch (Exception e)
					{
						Debug.LogError ("Don`t Can Init On Create {0}".AsFormat (instance));
						Debug.LogError (e.Message);
					}
					_instance.CreateAsset ("Assets/Resources/Data/{0}.asset".AsFormat (typeName));

				}
#if UNITY_2018_2_OR_NEWER
				var PreoloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets ().ToList ();
				if (!PreoloadedAssets.Contains (_instance))
				{
					PreoloadedAssets.Add (_instance);
					UnityEditor.PlayerSettings.SetPreloadedAssets (PreoloadedAssets.ToArray ());
				}
#endif
#endif
				_instance.InitOnLoaded ();
				return _instance;
			}
		}
		protected virtual void InitOnLoaded () { }

		[ContextMenu ("Create Asset")] public void CreateAsset () { this.CreateAsset ("Assets/Resources/Data/{0}.asset".AsFormat (typeName)); }
		#endregion
	}
}
