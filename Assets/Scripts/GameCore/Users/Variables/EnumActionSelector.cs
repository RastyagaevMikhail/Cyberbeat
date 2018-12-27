using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public abstract class EnumActionSelector<EnumType> : MonoBehaviour where EnumType : EnumScriptable
	{
		public List<TypeAction> actions = new List<TypeAction> ();
		Dictionary<EnumType, UnityEvent> selector = null;
		Dictionary<EnumType, UnityEvent> Selector { get { return selector ?? (selector = actions.ToDictionary (a => a.type as EnumType, a => a.action)); } }

		public virtual void Invoke (EnumType parameter)
		{
			Selector[parameter].Invoke ();
		}
		public Action this [EnumType parametr]
		{
			get { return Selector[parametr].Invoke; }
		}

	}

	[Serializable]
	public class TypeAction
	{
		public EnumScriptable type;
		public UnityEvent action;
	}
}
