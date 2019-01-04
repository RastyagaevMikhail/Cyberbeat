using UnityEngine;

namespace GameCore
{
	public abstract class ASavableVariable : ScriptableObject
	{
		public abstract bool isSavable { get; set; }
		public abstract string CategoryTag { get; set; }
		public abstract void SaveValue ();
		public abstract void LoadValue ();
		public abstract void ResetDefault ();
#if UNITY_EDITOR
		public abstract void ResetLoaded ();
		public abstract void CreateAsset (string path);
#endif
	}
}
