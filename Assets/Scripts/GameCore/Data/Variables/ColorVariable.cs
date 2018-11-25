using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "", menuName = "Variables/Color")]
	public class ColorVariable : SavableVariable<Color>
	{
		static string DefaultValue { get { return JsonUtility.ToJson (Color.white); } }
		public override void LoadValue ()
		{
			base.LoadValue ();
			Value = JsonUtility.FromJson<Color> (PlayerPrefs.GetString (name, DefaultValue));
		}

		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, JsonUtility.ToJson (Value));
		}

	}
}
