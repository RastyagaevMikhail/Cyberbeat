using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class Saver : MonoBehaviour
	{
		SaveData saveData { get { return SaveData.instance; } }
		private void OnApplicationQuit ()
		{
			saveData.SaveAll ();
		}

	}
}
