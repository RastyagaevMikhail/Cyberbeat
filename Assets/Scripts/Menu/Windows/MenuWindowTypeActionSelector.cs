using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class MenuWindowTypeActionSelector : EnumActionSelector<MenuWindowType>
	{
		public void InvokeByType (MenuWindowType menuWindowType)
		{
			this[menuWindowType]();
		}
		public void _InvokeInUnityObjectVariable (UnityObjectVariable variable)
		{
			Invoke (variable.As<MenuWindowType> ());
		}
	}
}
