namespace GameCore
{
	public abstract class TransformGroupSelector<TEnumType> : AEnumDataSelectorMonoBehaviour<TEnumType, TransformGroup> where TEnumType : EnumScriptable
	{
		public override TransformGroup this [TEnumType enumType]
		{
			get
			{
				return base[enumType] as TransformGroup;
			}
		}
	}
}
