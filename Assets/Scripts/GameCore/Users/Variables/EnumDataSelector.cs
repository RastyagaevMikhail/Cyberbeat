using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{

	public abstract class EnumDataSelector<EnumType, DataType>
		: SerializedMonoBehaviour, IEnumerable<KeyValuePair<EnumType, DataType>>
		where EnumType : EnumScriptable
	where DataType : class

	{
		[HideReferenceObjectPicker]
		public List<TypeData> Datas = new List<TypeData> ();
		Dictionary<EnumType, DataType> selector = null;
		Dictionary<EnumType, DataType> Selector { get { return selector ?? (selector = Datas.ToDictionary (a => a.type, a => a.data)); } }
		public List<EnumType> Keys { get { return Datas.Select (d => d.type).ToList(); } }
		public List<DataType> Values { get { return Datas.Select (d => d.data).ToList(); } }
		public DataType GetData (EnumType type)
		{
			if (!Selector.ContainsKey (type)) return null;
			return Selector[type];
		}

		IEnumerator<KeyValuePair<EnumType, DataType>> IEnumerable<KeyValuePair<EnumType, DataType>>.GetEnumerator ()
		{

			return Datas.GetEnumerator () as IEnumerator<KeyValuePair<EnumType, DataType>>;
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return Datas.GetEnumerator ();
		}

		public DataType this [EnumType type]
		{
			get { return GetData (type); }
		}

		[System.Serializable]
		public class TypeData : EmptyData
		{
			public EnumType type;
			public DataType data;
		}
	}

	[System.Serializable] public class EmptyData { }
}
