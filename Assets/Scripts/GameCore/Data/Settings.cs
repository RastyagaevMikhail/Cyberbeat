using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	using UnityEngine;

	public class Settings : SingletonData<Settings>
	{

#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/Settings")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void ResetDefault () { }
		public override void InitOnCreate () { }
#endif		
public override bool Initialized { get { return instance; } }
		public override void Initialize ()
		{

		}
		public float SoundVolume { get { return SoundManager.globalSoundsVolume; } set { SoundManager.globalSoundsVolume = value; } }
		public float MusicVolume { get { return SoundManager.globalMusicVolume; } set { SoundManager.globalMusicVolume = value; } }
		public float UISoundsVolume { get { return SoundManager.globalUISoundsVolume; } set { SoundManager.globalUISoundsVolume = value; } }
		public float GlobalVolume { get { return SoundManager.globalVolume; } set { SoundManager.globalVolume = value; } }

		public bool MuteGameSound { get { return SoundManager.MuteSound; } set { SoundManager.MuteSound = value; } }
		public bool MuteUISound { get { return SoundManager.MuteUISound; } set { SoundManager.MuteUISound = value; } }
		public bool MuteSound { get { return MuteGameSound && MuteUISound; } set { MuteGameSound = MuteUISound = value; } }
		public bool MuteMusic { get { return SoundManager.MuteMusic; } set { SoundManager.MuteMusic = value; } }

		public SystemLanguage Language { get { return Localizator.GetLanguage (); } set { Localizator.SetLanguage (value); } }
		// public InputType inputType;
	}
}
