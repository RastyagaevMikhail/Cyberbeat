using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class Saver : MonoBehaviour
	{
		SaveData saveData => Resources.Load<SaveData> ("Data/SaveData");
		[SerializeField] bool debug;
		private void OnApplicationQuit ()
		{
			if (debug)
			{
				Debug.Log ($"Saver.OnApplicationQuit()");
				Debug.Log (saveData, saveData);
			}
			saveData.SaveAll ();
		}

		private void OnApplicationPause (bool pauseStatus)
		{
			if (debug)
			{
				Debug.Log ($"Saver.OnApplicationPause({pauseStatus})");
				Debug.Log (saveData, saveData);
			}
			saveData.SaveAll ();
		}

	}
}
