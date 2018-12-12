using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	[CreateAssetMenu (fileName = "Vector3Variable", menuName = "Variables/GameCore/Vector3")]
	public class Vector3Variable : SavableVariable<Vector3>
	{
	#if UNITY_EDITOR
		public override void ResetDefault ()
        {
            if (ResetByDefault)
            {
                Value = DefaultValue;
                SaveValue ();
            }
        }
	#endif
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
