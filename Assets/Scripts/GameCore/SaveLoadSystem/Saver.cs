using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class Saver : MonoBehaviour
	{
		public static bool IsApplicationQuiting = false;
		SaveData saveData { get { return SaveData.instance; } }
		private void OnApplicationQuit ()
		{
			IsApplicationQuiting = true;
			saveData.SaveAll ();
		}

	}
}
