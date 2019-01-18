using GameCore;

using UnityEngine;
namespace CyberBeat
{
	[System.Serializable]
	public class MusicInfo
	{
		public AudioClip clip;
		public string AuthorName;
		public string TrackName;
		public Sprite AlbumImage;
		public TracksCollection data { get { return TracksCollection.instance; } }

		#if UNITY_EDITOR
		public void Validate (string NameTrack)
		{
			clip = Tools.GetAssetAtPath<AudioClip> ("Assets/Audio/Tracks/{0}.mp3".AsFormat (NameTrack));
			var SpitedName = NameTrack.Split ("-".ToCharArray (), System.StringSplitOptions.RemoveEmptyEntries);
			AuthorName = SpitedName[0].TrimEnd ();;
			TrackName = SpitedName[1].TrimStart ();
			AlbumImage = Tools.GetAssetAtPath<Sprite> ("Assets/Sprites/UI/AlbumPhoto/{0}.png".AsFormat (NameTrack));
		}
		#endif
	}
}
