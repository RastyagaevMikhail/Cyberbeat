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

		[SerializeField, Sirenix.OdinInspector.DrawWithUnity] List<StateControl> States;
		[SerializeField] Image stateImage;
		[SerializeField] LocalizeTextMeshProUGUI stateName;
		[SerializeField] Image AddIcon;
		int currentStateIndex = 0;
		public void ToggleState ()
		{
			currentStateIndex++;
			LoadState (currentStateIndex);

		}
		public void LoadState (int stateIndex)
		{
			stateIndex %= States.Count;
			StateControl state = States[stateIndex];

			state.action.Invoke ();

			bool useAddIcon = state.UseAddIcon;

			if (AddIcon)
				AddIcon.gameObject.SetActive (useAddIcon);
			(state.UseAddIcon ? AddIcon : stateImage).sprite = state.icon;

			stateName.Id = state.name;
			stateName.UpdateText ();
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
