using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{

	public abstract class DataCollections<T, K> : SingletonData<T> where T : SingletonData<T> where K : class
	{
#if UNITY_EDITOR
		// [UnityEditor.MenuItem ("Game/Data/Collections/{0}")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void InitOnCreate ()
		{

		}
		public override void ResetDefault ()
		{

		}
#else
		public override void ResetDefault () { }
#endif
		public List<K> Objects = new List<K> ();
	}
}
