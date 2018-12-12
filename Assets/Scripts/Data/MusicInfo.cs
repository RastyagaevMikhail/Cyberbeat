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
	}
}
