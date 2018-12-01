﻿using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class TimeSpanTimerAction : MonoBehaviour
	{
		[InlineButton ("reset")]
		[SerializeField] TimeSpanVariable variable;
		[SerializeField] GameEvent RestartOn;
		[SerializeField] bool Started = false;
		[SerializeField] DateTimeVariable lastDisabled;
		EventListener listener;

		[SerializeField, DrawWithUnity] UnityEvent action;
		public void reset ()
		{
			variable.Reset ().AddSeconds (1);
		}
		private void OnEnable ()
		{
			// Debug.Log ("OnEnable Timer");
			if (listener == null) listener = new EventListener (RestartOn, StartTimer);
			listener.OnEnable ();
			if (!lastDisabled.isNew)
			{
				// Debug.LogFormat ("lastDisabled.Value = {0}", lastDisabled.Value);
				TimeSpan ts = lastDisabled - DateTime.Now;
				// Debug.LogFormat ("ts = {0}", ts);
				int totalSeconds = (int) ts.TotalSeconds;
				// Debug.LogFormat ("totalSeconds = {0}", totalSeconds);
				variable.AddSeconds (totalSeconds);
				if (!ChechComplete ())
					if (Started)
						StartTimer ();
			}
		}

		private bool ChechComplete ()
		{
			bool IsCompeted = variable.Value.TotalSeconds <= 0;
			if (IsCompeted)
			{
				action.Invoke ();
				Started = false;
			}
			return IsCompeted;

		}

		private void OnDisable ()
		{
			// Debug.Log ("OnDisable Timer");
			listener.OnDisable ();
			lastDisabled.Value = DateTime.Now;
			// Debug.LogFormat ("lastDisabled.Value = {0}", lastDisabled.Value);
		}
		private void Start ()
		{
			StartTimer ();
		}
		public void StartTimer ()
		{
			Started = true;
			StartCoroutine (cr_StartTimer ());

		}
		private IEnumerator cr_StartTimer ()
		{
			WaitForSeconds wts = new WaitForSeconds (1f);
			while (variable.Value.TotalSeconds > 0)
			{
				// Debug.Log (variable.Value);
				yield return wts;
				variable.AddSeconds (-1);
			}
			ChechComplete ();
			yield return null;
		}
	}
}
