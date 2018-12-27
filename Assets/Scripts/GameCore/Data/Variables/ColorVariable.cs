using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (fileName = "", menuName = "Variables/GameCore/Color")]
	public class ColorVariable : SavableVariable<Color>
	{
		public override void ResetDefault ()
		{
			if (ResetByDefault)
			{
				Value = DefaultValue;
				SaveValue ();
			}
		}
		static string DefaultValueStr { get { return JsonUtility.ToJson (Color.white); } }
		public override void LoadValue ()
		{
			base.LoadValue ();
			Value = JsonUtility.FromJson<Color> (PlayerPrefs.GetString (name, DefaultValueStr));
		}

		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, JsonUtility.ToJson (Value));
		}

		public void SetValue (Color color)
		{
			Value = color;
		}
	}
}

