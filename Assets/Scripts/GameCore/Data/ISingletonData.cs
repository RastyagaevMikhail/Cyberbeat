namespace GameCore
{
	public interface ISingletonData
	{
#if UNITY_EDITOR
		void ResetDefault ();
		void InitOnCreate ();
#endif

	}
}
