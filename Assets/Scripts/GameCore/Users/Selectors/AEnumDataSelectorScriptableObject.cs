using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameCore
{
    public abstract class AEnumDataSelectorScriptableObject<TEnumType, TDataType> : ASelectorScriptableObject,
        IEnumDataSelector<TEnumType, TDataType>
        // where TEnumType : EnumScriptable
        // where TDataType : class
        {
            public virtual TDataType this [TEnumType type]
            {
                get { return GetData (type); }
                set { Selector[type] = value; }
            }
            public TDataType GetData (TEnumType type)
            {
                TDataType value = default (TDataType);
                Selector.TryGetValue (type, out value);
                return value;
            }

            public virtual void Add (TEnumType type, TDataType data)
            {
                Selector.Add (type, data);
            }
            
            public bool ContainsKey (TEnumType type)
            {
                return Selector.ContainsKey (type);
            }

            public abstract List<TypeData<TEnumType, TDataType>> Datas { get; }
            public override void OnEnable ()
            {
                selector = Datas.ToDictionary (a => a.type, a => a.data);
                // Debug.Log ($"OnEneable Selector {name}\n{Tools.LogCollection(selector)}", this);
            }
            Dictionary<TEnumType, TDataType> selector = null;
            public Dictionary<TEnumType, TDataType> Selector { get { return selector ?? (selector = Datas.ToDictionary (a => a.type, a => a.data)); } }

            public List<TEnumType> Keys { get { return Datas.Select (d => d.type).ToList (); } }

            public List<TDataType> Values { get { return Datas.Select (d => d.data).ToList (); } }

        }
}
