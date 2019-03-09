using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{

	public class KeyboardManger : MonoBehaviour
	{

		[SerializeField] List<KeyEvent> keyEvents = new List<KeyEvent> ();
		[SerializeField] List<KeyAction> keyActions = new List<KeyAction> ();
		// Update is called once per frame
		void Update ()
		{
			foreach (var ke in keyEvents)
				ke.TriggerEvent ();
			foreach (var ka in keyActions)
				ka.TriggerEvent ();
		}

		[Serializable]
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

		[Serializable]
		public class KeyAction
		{
			public UnityEvent action;
			public KeyCode key;
			public void TriggerEvent ()
			{
				if (Input.GetKeyDown (key))
				{
					action.Invoke ();
				}
			}
		}
	}
}
