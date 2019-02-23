namespace GameCore
{
	public interface ISingletonData
#if UNITY_EDITOR
		: IResetable
#endif
	{
#if UNITY_EDITOR
		void InitOnCreate ();
#endif
	}
}
