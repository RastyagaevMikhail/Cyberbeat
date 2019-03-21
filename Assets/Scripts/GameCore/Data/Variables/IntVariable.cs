using Sirenix.OdinInspector;

using System;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "", menuName = "Variables/GameCore/Int")]
	public class IntVariable : SavableVariable<int>
	{
		[Button]
		public override void ResetDefault ()
		{
			if (ResetByDefault)
			{
				Value = DefaultValue;
				this.Save ();
				SaveValue ();
			}
		}
		public void SetValue (int value)
		{
			Value = value;
		}
		public void SetValue (float value)
		{
			Value = (int) value;
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

		[Button]
		[ContextMenu ("Save Value")]
		public override void SaveValue ()
		{
			// Debug.LogFormat("Save = {0}", name);
			try
			{
				PlayerPrefs.SetInt (name, _value);
			}
			catch (System.Exception)
			{
				Debug.Log (this, this);
				#if UNITY_EDITOR
					Debug.Log(UnityEditor.AssetDatabase.GetAssetPath(this));
				#endif
				throw;
			}
		}

		[Button]
		[ContextMenu ("Load Value")]
		public override void LoadValue ()
		{
			base.LoadValue ();
			_value = PlayerPrefs.GetInt (name, DefaultValue);
		}

		public void Increment ()
		{
			Value++;
		}
		public void Decrement ()
		{
			Value--;
		}
		public static IntVariable operator + (IntVariable variable, int other)
		{
			variable.Value += other;
			return variable;
		}

		public static IntVariable operator - (IntVariable variable, int other)
		{
			variable.Value -= other;
			return variable;
		}

		public bool IsZero ()
		{
			return Value == 0;
		}

		public float AsFloat ()
		{
			return Value;
		}
#if UNITY_EDITOR
		[ShowInInspector] string savedValue => PlayerPrefs.GetInt (name, 0).ToString ();
#endif
	}
}
