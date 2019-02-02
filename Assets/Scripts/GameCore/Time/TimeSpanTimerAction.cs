using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class TimeSpanTimerAction : MonoBehaviour
	{
		[ContextMenuItem ("Reset to Zero", "ResetToZero")]
		[ContextMenuItem ("Reset to Defaut", "ResetToDefault")]
		[InlineButton ("ResetToZero", "Z")]
		[InlineButton ("ResetToDefault", "D")]
		[SerializeField] TimeSpanVariable variable;
		[SerializeField] UnityEvent action;
		public void ResetToZero ()
		{
			variable.Reset ().AddSeconds (1);
		}
		public void ResetToDefault ()
		{
			variable.ResetDefault ();
		}

		private bool ChechComplete ()
		{
			bool IsCompeted = variable.Value.TotalSeconds <= 0;
			if (IsCompeted)
			{
				action.Invoke ();
			}
			return IsCompeted;
		}

		private void Start ()
		{
			StartTimer ();
		}

		[Button]

		public void StartTimer ()
		{
			if (ChechComplete ()) return;
			StartCoroutine (cr_StartTimer ());
		}
		private IEnumerator cr_StartTimer ()
		{
			WaitForSeconds wts = new WaitForSeconds (1f);
			while (variable.Value.TotalSeconds > 0)
			{
				yield return wts;
				variable.AddSeconds (-1);
			}
			ChechComplete ();
		}
	}
}
