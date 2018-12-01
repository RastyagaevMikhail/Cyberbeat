using System;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "", menuName = "Variables/GameCore/Int")]
	public class IntVariable : SavableVariable<int>
	{

		public void SetValue (int value)
		{
			Value = value;
		}
		public IntVariable Init (int value)
		{
			Value = value;
			return this;
		}

		public void SetValue (IntVariable value)
		{
			Value = value.Value;
		}

		public void ApplyChange (int amount)
		{
			Value += amount;
		}

		public void ApplyChange (IntVariable amount)
		{
			Value += amount.Value;
		}

		public override void SaveValue ()
		{
			// Debug.LogFormat("Save = {0}", name);
			PlayerPrefs.SetInt (name, _value);
		}
		public override void LoadValue ()
		{
			base.LoadValue ();
			_value = PlayerPrefs.GetInt (name, 0);
		}

		public void Increment ()
		{
			Value++;
		}
		public void Decrement ()
		{
			Value--;
		}
		public static IntVariable operator +(IntVariable variable, int other)
		{
			variable.Value += other;
			return variable;
		}
		public static IntVariable operator -(IntVariable variable, int other)
		{
			variable.Value -= other;
			return variable;
		}
	}
}
