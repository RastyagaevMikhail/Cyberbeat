using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "DateTime Variable", menuName = "Variables/GameCore/DateTime")]
	public class DateTimeVariable : SavableVariable<DateTime>
	{
		public override void ResetDefault ()
		{
			if (ResetByDefault)
			{
				Value = CustomDefault;
				SaveValue ();
			}
		}

		[SerializeField] CustomDateTime CustomDefault;
		string strDeafultValue { get { return new DateTime ().ToString (); } }

		public bool isNew { get { return _value == new DateTime (); } }

		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, Value.ToString ());
		}
		public override void LoadValue ()
		{
			base.LoadValue ();
			var str = PlayerPrefs.GetString (name, strDeafultValue);
			DateTime.TryParse (str, out _value);
		}

		public static implicit operator DateTimeVariable (DateTime value)
		{
			DateTimeVariable dateTimeVariable = CreateInstance<DateTimeVariable> ();
			dateTimeVariable._value = value;
			return dateTimeVariable;
		}

		public static implicit operator DateTime (DateTimeVariable value)
		{
			return value._value;
		}

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
	public class CustomDateTime
	{
		public int Year;
		public int Mounth;
		public int Day;
		public int Hour;
		public int Minute;
		public int Second;

		public static implicit operator CustomDateTime (DateTime value)
		{
			return new CustomDateTime () { Year = value.Year, Mounth = value.Month, Day = value.Day, Hour = value.Hour, Minute = value.Minute, Second = value.Second };
		}

		public static implicit operator DateTime (CustomDateTime value)
		{
			return new DateTime (value.Year, value.Mounth, value.Day, value.Hour, value.Minute, value.Second);
		}
	}
}
