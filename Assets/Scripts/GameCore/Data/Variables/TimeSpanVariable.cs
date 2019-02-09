using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "TimeSpanVariable", menuName = "Variables/GameCore/TimeSpan")]
	public class TimeSpanVariable : SavableVariable<TimeSpan>
	{
		[Button]
		[ContextMenu ("ResetDefault")]
		public override void ResetDefault ()
		{
			if (ResetByDefault)
			{
				Value = CustomDefault;
				SaveValue ();
			}
		}

		[SerializeField] CustomTimeSpan CustomDefault;
		[Button]
		public override void LoadValue ()
		{
			base.LoadValue ();
			var strTime = PlayerPrefs.GetString (name, new TimeSpan ().ToString ());
			TimeSpan.TryParse (strTime, out _value);
		}

		[Button]
		[ContextMenu ("SaveValue")]
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

		[ContextMenu ("Reset")]
		public TimeSpanVariable Reset ()
		{
			Value = new TimeSpan ();
			return this;
		}
#if UNITY_EDITOR
		[ShowInInspector] string savedValue => PlayerPrefs.GetString (name, string.Empty);

#endif
		public bool IsZero => Value.TotalSeconds == 0;
		[ContextMenu ("ShowValue")]
		void ShowValue ()
		{
			Debug.Log (Value);
		}

		[ContextMenu ("Toggle Savable")]
		void ToggleSavable () { isSavable = !isSavable; }

		[ContextMenu ("Check Savable")]
		void CheckSavable () { Debug.LogFormat ("{0} isSavable = {1}", name, isSavable); }
	}

	[Serializable]
	public class CustomTimeSpan
	{
		public int seconds;
		public int minutes;
		public int hours;
		public static implicit operator CustomTimeSpan (TimeSpan value)
		{
			return new CustomTimeSpan () { seconds = value.Seconds, minutes = value.Minutes, hours = value.Hours };
		}

		public static implicit operator TimeSpan (CustomTimeSpan value)
		{
			return new TimeSpan (value.hours, value.minutes, value.seconds);
		}
	}
}
