using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "Vector3Variable", menuName = "Variables/GameCore/Vector3")]
	public class Vector3Variable : SavableVariable<Vector3>
	{
		public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
		static string DefaultValueStr { get { return JsonUtility.ToJson (Vector3.zero); } }
		public override void LoadValue ()
		{
			base.LoadValue ();
			Value = JsonUtility.FromJson<Vector3> (PlayerPrefs.GetString (name, DefaultValueStr));
		}

		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, JsonUtility.ToJson (Value));
		}

	}
}
