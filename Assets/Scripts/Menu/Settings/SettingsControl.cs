using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Text = TMPro.TextMeshProUGUI;
using GameCore;

namespace CyberBeat
{
	public class SettingsControl : MonoBehaviour
	{
		[SerializeField] List<StateControl> States;
		[SerializeField] Image stateImage;
		[SerializeField] LocalizeTextMeshProUGUI stateName;
		[SerializeField] Image AddIcon;
		int currentStateIndex = 0;
		public void ToggleState ()
		{
			currentStateIndex++;
			currentStateIndex %= States.Count;
			SetState (currentStateIndex);
		}
		public void SetState (int stateIndex)
		{
			SetState (stateIndex, true, false);
		}
		public void SetStateWithoutInvoke (int stateIndex)
		{
			SetState (stateIndex, false, false);
		}
		public void SetStateWithOverrideIndex (int stateIndex)
		{
			SetState (stateIndex, true, true);
		}
		public void SetState (int stateIndex, bool invoke = true, bool overrideStateIndex = false)
		{
			stateIndex %= States.Count;
			StateControl state = States[stateIndex];

			if (invoke)
				state.action.Invoke ();

			bool useAddIcon = state.UseAddIcon;

			if (AddIcon)
				AddIcon.gameObject.SetActive (useAddIcon);
			(state.UseAddIcon ? AddIcon : stateImage).sprite = state.icon;

			stateName.SetID (state.name);
			if (overrideStateIndex) currentStateIndex = stateIndex;
		}

		[Serializable]
		public class StateControl
		{
			public string name;
			public Sprite icon;
			public bool UseAddIcon;
			public UnityEvent action;
		}
	}
}
