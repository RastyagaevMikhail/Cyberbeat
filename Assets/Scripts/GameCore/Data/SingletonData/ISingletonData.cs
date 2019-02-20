namespace GameCore
{
	public interface ISingletonData: IResetable
	{
#if UNITY_EDITOR
		void InitOnCreate ();
#endif
	}
}
