using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Text = TMPro.TextMeshProUGUI;
using GameCore;
namespace CyberBeat
{
	public class SettingsControl : SerializedMonoBehaviour
	{

		[OdinSerialize] List<StateControl> States;
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

			state.Invoke ();

			bool useAddIcon = state.UseAddIcon;

			if (AddIcon)
				AddIcon.gameObject.SetActive (useAddIcon);
			(state.UseAddIcon ? AddIcon : stateImage).sprite = state.icon;

			stateName.Id = state.name;
			stateName.UpdateText();
		}

		[Serializable]
		public class StateControl
		{
			[SerializeField] Action action;
			public void Invoke ()
			{
				if (action != null) action ();
			}
			public string name;
			public Sprite icon;
			public bool UseAddIcon;
		}
	}
}
