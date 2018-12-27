﻿using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class GameSettings : MonoBehaviour
	{
		public Settings settings { get { return Settings.instance; } }
		public bool MuteMusic { get { return settings.MuteMusic; } set { settings.MuteMusic = value; } }
		public bool MuteSound { get { return settings.MuteSound; } set { settings.MuteSound = value; } }
		public bool VibrationEnabled { get { return settings.VibrationEnabled; } set { settings.VibrationEnabled = value; if (value) Vibration.Vibrate (settings.VibrationTime); } }
		public InputType inputType { get { return settings.inputType; } set { settings.inputType = value; } }
		public void SetInputType (int type)
		{
			inputType = (InputType) type;
		}
		public LocalizationManager localizator { get { return LocalizationManager.instance; } }
		public SystemLanguage Language { get { return localizator.currentLanguage; } set { localizator.SetLanguage (value); } }
		public void SetEnglish ()
		{
			Language = SystemLanguage.English;
		}
		public void SetRussian ()
		{
			Language = SystemLanguage.Russian;
		}

		[SerializeField] SettingsControl Music;
		[SerializeField] SettingsControl Sound;
		[SerializeField] SettingsControl VibrationControl;
		[SerializeField] SettingsControl LanguageControl;
		[SerializeField] SettingsControl Quality;
		[SerializeField] SettingsControl Control;
		private void Start ()
		{
			Music.SetState (MuteMusic ? 1 : 0);
			Sound.SetState (MuteSound ? 1 : 0);
			VibrationControl.SetState (settings.VibrationEnabled ? 0 : 1, false);
			Control.SetState ((int) inputType);

			LanguageControl.SetState (Language == SystemLanguage.Russian ? 1 : 0, true, true);
		}
		public void OpenVibrationTest ()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene ("VibrationTest");
		}
	}

}
