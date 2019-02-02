using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class TimeSpanVariablesSetter : VariableTextSetter<TimeSpanVariable, TimeSpan>
	{
		protected override void OnValueChanged (TimeSpan timeSpan)
		{

			text = string.Format (stringFormat, new DateTime (timeSpan.Ticks > 0 ? timeSpan.Ticks : 0));
		}
	}
}
