using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[ExecuteInEditMode]
	public class TimeSpanVariablesSetter : VariableTextSetter<TimeSpanVariable, TimeSpan>
	{
		protected override void OnValueChanged (TimeSpan obj)
		{
			text = string.Format ("{0}:{1}:{2}", obj.Hours, obj.Minutes, obj.Seconds);
		}
	}
}
