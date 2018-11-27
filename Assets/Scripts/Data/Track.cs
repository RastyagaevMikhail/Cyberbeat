using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "Track", menuName = "CyberBeat/Track", order = 0)]
	public class Track : ScriptableObject
	{
		public AudioClip clip;
		public string AuthorName;
		public string TrackName;
		public Sprite AlbumImage;

		public Sprite SocialIcon;
		public string SocialURL;
		public int Price = 2000;
		string SaveKey { get { return "Track {0} Buyed".AsFormat (name); } }
		public bool Buyed { get { return Tools.GetBool (SaveKey, false); } set { Tools.SetBool (SaveKey, value); } }

		public TracksCollection data { get { return TracksCollection.instance; } }
		public GameData gameData { get { return GameData.instance; } }
		public int TrackNumber { get { return data.Objects.IndexOf (this) + 1; } }

		public bool PlayByWatch;

		internal void SetMeAsCurrent ()
		{
			data.CurrentTrack = this;
		}

		internal void LoadScene ()
		{
			LoadingManager.instance.LoadScene (name);
		}

		internal bool TryBuy ()
		{
			Buyed = gameData.TryBuy (Price);
			return Buyed;
		}
	}
}
