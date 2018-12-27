using UnityEngine;

namespace GameCore
{
	public abstract class ASavableVariable : ScriptableObject
	{
		public abstract bool isSavable { get; set; }
		public string CategoryTag { get; set; }
		public abstract void SaveValue ();
		public abstract void LoadValue ();
#if UNITY_EDITOR
		public abstract void ResetDefault ();
		public abstract void ResetLoaded ();
		public abstract void CreateAsset (string path);
#endif
	}
}
