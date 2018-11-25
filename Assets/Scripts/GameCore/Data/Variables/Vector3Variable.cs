using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "Vector3Variable", menuName = "Variables/GameCore/Vector3")]
	public class Vector3Variable : SavableVariable<Vector3>
	{
		static string DefaultValue { get { return JsonUtility.ToJson (Vector3.zero); } }
		public override void LoadValue ()
		{
			base.LoadValue ();
			Value = JsonUtility.FromJson<Vector3> (PlayerPrefs.GetString (name, DefaultValue));
		}

		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, JsonUtility.ToJson (Value));
		}

	}
}
