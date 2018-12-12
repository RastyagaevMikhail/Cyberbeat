namespace GameCore
{
	public interface ISavableVariable
	{
		bool isSavable { get; set; }
		string CategoryTag { get;set; }
		void SaveValue ();
		void LoadValue ();
#if UNITY_EDITOR
		void ResetDefault ();
		void ResetLoaded ();
		void CreateAsset (string path);
#endif
	}
}
