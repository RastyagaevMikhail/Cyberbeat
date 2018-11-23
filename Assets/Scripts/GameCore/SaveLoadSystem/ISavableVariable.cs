namespace GameCore
{
	public interface ISavableVariable
	{
		bool isSavable { get; set; }
		string CategoryTag { get; }
		void SaveValue ();
		void LoadValue ();
#if UNITY_EDITOR
		void ResetLoaded ();
		void CreateAsset (string path);
#endif
	}
}
