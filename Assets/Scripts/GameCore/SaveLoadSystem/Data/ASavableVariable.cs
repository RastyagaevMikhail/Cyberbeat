using UnityEngine;

namespace GameCore
{
	public abstract class ASavableVariable : ScriptableObject, IResetable
	{
		public abstract bool IsSavable { get;}
		public abstract void SaveValue ();
		public abstract void LoadValue ();
		public abstract void ResetDefault ();
#if UNITY_EDITOR
		public abstract void SetSavable(bool value);
		public abstract void ResetLoaded ();
		public abstract void CreateAsset (string path, bool IsSaveble);
#endif
	}
}
