using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class GameSettings : MonoBehaviour
	{
		public Settings settings { get { return Settings.instance; } }
		public bool MuteMusic { get { return settings.MuteMusic; } set { settings.MuteMusic = value; } }
		public void EnableMusic () { MuteMusic = false; }
		public void DisableMusic () { MuteMusic = true; }
		public bool MuteSound { get { return settings.MuteSound; } set { settings.MuteSound = value; } }
		public void EnableSound () { MuteSound = false; }
		public void DisableSound () { MuteSound = true; }

		public void SetEnglish ()
		{
			Localizator.LocalizatorSetEnglish ();
		}
		public void SetRussian ()
		{
			Localizator.LocalizatorSetRussian ();
		}

	}
}
