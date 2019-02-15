using System;
using System.Collections;
using System.Collections.Generic;
using CyberBeat;
using UnityEngine;

namespace GameCore
{
	public class Settings : SingletonData<Settings>
	{

#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/Settings")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void ResetDefault () { }
		public override void InitOnCreate () { }
#endif		
		public float SoundVolume { get { return SoundManager.globalSoundsVolume; } set { SoundManager.globalSoundsVolume = value; } }
		public float MusicVolume { get { return SoundManager.globalMusicVolume; } set { SoundManager.globalMusicVolume = value; } }

		internal static object Find (Func<object, bool> p)
		{
			throw new NotImplementedException ();
		}

		public float UISoundsVolume { get { return SoundManager.globalUISoundsVolume; } set { SoundManager.globalUISoundsVolume = value; } }
		public float GlobalVolume { get { return SoundManager.globalVolume; } set { SoundManager.globalVolume = value; } }

		public bool MuteGameSound { get { return SoundManager.MuteSound; } set { SoundManager.MuteSound = value; } }
		public bool MuteUISound { get { return SoundManager.MuteUISound; } set { SoundManager.MuteUISound = value; } }
		public bool MuteSound { get { return MuteGameSound && MuteUISound; } set { MuteGameSound = MuteUISound = value; } }
		public bool MuteMusic { get { return SoundManager.MuteMusic; } set { SoundManager.MuteMusic = value; } }
		public LocalizationManager localizator { get { return LocalizationManager.instance; } }
		public SystemLanguage Language { get { return localizator.currentLanguage; } set { localizator.SetLanguage (value); } }

		public bool VibrationEnabled { get { return PlayerPrefs.GetInt ("Vibration", 1) == 1; } set { PlayerPrefs.SetInt ("Vibration", value ? 1 : 0); } }
		public long VibrationTime { get { return (long) PlayerPrefs.GetFloat ("VibrationTime", 55); } set { PlayerPrefs.SetFloat ("VibrationTime", value); } }

	

	}
	
}
