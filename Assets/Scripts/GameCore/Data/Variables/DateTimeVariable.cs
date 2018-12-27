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
				Value = DefaultValue;
				SaveValue ();
			}
		}
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

	}
}
