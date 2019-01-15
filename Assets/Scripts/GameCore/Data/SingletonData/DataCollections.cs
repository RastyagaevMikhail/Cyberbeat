using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{

	public abstract class DataCollections<TSingletonData, TObjects> : SingletonData<TSingletonData> where TSingletonData : SingletonData<TSingletonData> where TObjects : class
	{
#if UNITY_EDITOR
		// [UnityEditor.MenuItem ("Game/Data/Collections/{0}")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void InitOnCreate ()
		{

		}
		public override void ResetDefault ()
		{

		}
#endif
		public List<TObjects> Objects = new List<TObjects> ();
	}
}
