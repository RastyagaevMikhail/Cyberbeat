using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{

	public interface IEnumDataSelector<TEnumType, TDataType> : ISelector
		// where TEnumType : EnumScriptable
	where TDataType : class

	{
		// public abstract List<TypeData<TEnumType, TDataType>> Datas { get; }
		List<TypeData<TEnumType, TDataType>> Datas { get; }
		// Dictionary<TEnumType, TDataType> selector = null;
		// Dictionary<TEnumType, TDataType> Selector { get { return selector ?? (selector = Datas.ToDictionary (a => a.type, a => a.data)); } }
		Dictionary<TEnumType, TDataType> Selector { get; }
		// public List<TEnumType> Keys { get { return Datas.Select (d => d.type).ToList (); } }
		List<TEnumType> Keys { get; }
		// public List<TDataType> Values { get { return Datas.Select (d => d.data).ToList (); } }
		List<TDataType> Values { get; }
		// public TDataType GetData (TEnumType type)
		// {
		// 	if (!Selector.ContainsKey (type)) return null;
		// 	return Selector[type];
		// }
		TDataType GetData (TEnumType type);

		// public virtual TDataType this [TEnumType type]
		// {
		// 	get { return GetData (type); }
		// }
		TDataType this [TEnumType type] { get;set; }

		void Add(TEnumType type,TDataType data);
		bool ContainsKey(TEnumType type);

	}

	[Serializable]
	public abstract class TypeData<TEnumType, TDataType> : EmptyData
	// where TEnumType : EnumScriptable
	where TDataType : class
	{
		public TEnumType type;
		public TDataType data;
	}

	[Serializable]
	public class EmptyData { }

}
