using System;

using UnityEngine;

namespace CyberBeat
{

	[System.Serializable]
	public class VideoInfo
	{
		[SerializeField] int videoCount = 3;
		[SerializeField] string SaveKeyVideoCount;
		[SerializeField] string SaveKeyLastDateVideoExpired;
		public DateTime LastDateVideoExpired
		{
			get => DateTime.Parse (PlayerPrefs.GetString (SaveKeyLastDateVideoExpired, DateTime.Now.ToString ()));
			set => PlayerPrefs.SetString (SaveKeyLastDateVideoExpired, value.ToString ());
		}
		public int VideoCount
		{
			get => PlayerPrefs.GetInt (SaveKeyVideoCount, videoCount);
			set
			{
				PlayerPrefs.SetInt (SaveKeyVideoCount, value);
				if (value <= 0) LastDateVideoExpired = DateTime.Now;
			}
		}
		public void CheckAvalivable ()
		{
			if (VideoCount > 0) return;

			var diff = DateTime.Now.Subtract (LastDateVideoExpired);
			if (diff.Days >= 1) VideoCount = videoCount;
		}
		public void Validate (string TrackName)
		{
			SaveKeyVideoCount = $"{TrackName} VideoCount";
			SaveKeyLastDateVideoExpired = $"{TrackName} LastDateVideoExpired";
		}
	}
}
