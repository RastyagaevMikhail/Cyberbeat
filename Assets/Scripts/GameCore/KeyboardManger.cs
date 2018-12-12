using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{

	public class KeyboardManger : MonoBehaviour
	{

		[SerializeField] List<KeyEvent> keyEvents = new List<KeyEvent> ();
		// Update is called once per frame
		void Update ()
		{
			foreach (var ke in keyEvents)
				ke.TriggerEvent ();
		}

		[System.Serializable]
		public class KeyEvent
		{
			public GameEvent Event;
			public KeyCode key;
			public void TriggerEvent ()
			{
				if (Input.GetKeyDown (key))
				{
					Event.Raise ();
				}
			}
		}
	}
}
