using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Serialization.Formatters;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "TimeSpanVariable", menuName = "Variables/GameCore/TimeSpan")]
	public class TimeSpanVariable : SavableVariable<TimeSpan>
	{
		public override void LoadValue ()
		{
			base.LoadValue ();
			var strTime = PlayerPrefs.GetString (name, new TimeSpan ().ToString ());
			TimeSpan.TryParse (strTime, out _value);
		}
		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, _value.ToString ());
		}

		public TimeSpanVariable AddSeconds (int seconds)
		{
			Value += new TimeSpan (0, 0, seconds);
			return this;
		}

		public TimeSpanVariable AddMinutes (int minutes)
		{
			Value += new TimeSpan (0, minutes, 0);
			return this;
		}
		public TimeSpanVariable AddHours (int hours)
		{
			Value += new TimeSpan (hours, 0, 0);
			return this;
		}

		internal TimeSpanVariable Add (TimeSpan ts)
		{
			Value += ts;
			return this;
		}

		[Button]
		public TimeSpanVariable Reset ()
		{
			Value = new TimeSpan ();
			return this;
		}
	}
}
