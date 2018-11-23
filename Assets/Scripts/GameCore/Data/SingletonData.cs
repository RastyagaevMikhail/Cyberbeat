using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;
using System.Collections;
using System.Collections.Generic;

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
					Tools.CreateAsset (_instance, "Assets/Resources/Data/{0}.asset".AsFormat (typeName));
				}
#endif
				return _instance;
			}
		}

		#endregion
	}
}
