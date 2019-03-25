using System;

using UnityEngine;
namespace CyberBeat
{
	[Serializable]
	public class VideoInfo
	{
		[SerializeField] string SaveKey_VideoCount;
		[SerializeField] int videoCount;

		public int VideoCount
		{
			get => PlayerPrefs.GetInt (SaveKey_VideoCount, videoCount);
			set
			{
				PlayerPrefs.SetInt (SaveKey_VideoCount, value);
				if (value <= 0) LastDataExpired = DateTime.Now;
			}
		}

		[SerializeField] string SaveKey_LastDataExpired;

		DateTime LastDataExpired
		{
			get
			{
				string defaultValue = DateTime.Now.ToString ();
				string savedString = PlayerPrefs.GetString (SaveKey_LastDataExpired, defaultValue);
				return DateTime.Parse (savedString);
			}
			set
			{
				string stringValue = value.ToString ();
				PlayerPrefs.SetString (SaveKey_LastDataExpired, stringValue);
			}
		}

		public void Validate (string trackName)
		{
			SaveKey_LastDataExpired = $"{trackName} LastDataExpired";
			SaveKey_VideoCount = $"{trackName} VideoCount";
		}

		public void CheckAcalivable ()
		{
			var diff = DateTime.Now.Subtract (LastDataExpired);
			
			if (diff.Days >= 1)
				VideoCount = videoCount;
		}
	}
}
