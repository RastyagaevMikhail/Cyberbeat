namespace GameCore
{
	public interface ISingletonData
	{
#if UNITY_EDITOR
		void ResetDefault ();
		void InitOnCreate ();
#endif
		void Initialize ();
		bool Initialized { get; }

	}
}
