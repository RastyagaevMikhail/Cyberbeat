using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "Track", menuName = "Cyberbeat/Track", order = 0)]
	public class Track : ScriptableObject
	{	
		public AudioClip clip;
		public string AuthorName;
		public string TrackName;
		public Sprite AlbulImage;

		public Sprite SocialIcon;
		public string SocialURL;

	}
}
