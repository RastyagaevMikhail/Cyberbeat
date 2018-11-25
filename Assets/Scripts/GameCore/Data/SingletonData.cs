using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
	public abstract class SingletonData<T> : SerializedScriptableObject, ISingletonData where T : SingletonData<T>
	{
		#region  instance
#if UNITY_EDITOR
		[Button] public abstract void ResetDefault ();
		[Button] public abstract void InitOnCreate ();
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
					_instance.InitOnCreate ();
					_instance.CreateAsset ("Assets/Resources/Data/{0}.asset".AsFormat (typeName));

				}
				var PreoloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets ().ToList ();
				if (!PreoloadedAssets.Contains (_instance))
				{
					PreoloadedAssets.Add (_instance);
					UnityEditor.PlayerSettings.SetPreloadedAssets(PreoloadedAssets.ToArray());
				}
#endif
				return _instance;
			}
		}

		#endregion
	}
}
