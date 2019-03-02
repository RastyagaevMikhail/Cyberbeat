using Sirenix.OdinInspector;

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

		[SerializeField] CustomDateTime customValue;
		[SerializeField] CustomDateTime CustomDefault;
		string strDeafultValue { get { return new DateTime ().ToString (); } }

		public bool isNew { get { return _value == new DateTime (); } }

		public override DateTime Value
		{
			get => _value = customValue;
			set
			{
				customValue = value;
				_value = value;
				if (OnValueChanged != null)
					OnValueChanged (value);
			}
		}

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
		public void SetValueAsNow ()
		{
			Value = DateTime.Now;
		}

		[ShowInInspector] string savedValue => Value.ToString ();
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
		[Range (1, 9999)]
		public int Year = 1;
		[Range (1, 12)]
		public int Mounth = 1;
		[Range (1, 31)]
		public int Day = 1;
		[Range (0, 23)]
		public int Hour;
		[Range (0, 59)]
		public int Minute;
		[Range (0, 59)]
		public int Second;
		public override string ToString ()
		{
			return base.ToString () + "\n" +
				$"Year:{Year}\n" +
				$"Mounth:{Mounth}\n" +
				$"Day:{Day}\n" +
				$"Hour:{Hour}\n" +
				$"Minute:{Minute}\n" +
				$"Second:{Second}\n";
		}
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
