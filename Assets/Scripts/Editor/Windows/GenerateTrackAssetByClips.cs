using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
namespace CyberBeat.Editor
{
	using CyberBeat;

	using GameCore;

	using SonicBloom.Koreo;

	using System.Linq;
	public class GenerateTrackAssetByClips : OdinEditorWindow
	{

		[UnityEditor.MenuItem ("Game/Windows/GenerateTrackAssetByClips")]
		private static void OpenWindow ()
		{
			GetWindow<GenerateTrackAssetByClips> ().Show ();
		}

		[Button] public void Validate ()
		{
			clips = Tools.GetAtPath<AudioClip> ("Assets/Audio/Music/Tracks").ToList ();
		}
		public List<AudioClip> clips;

		[Button] public void Generate ()
		{
			foreach (var clip in clips)
			{
				var track = ScriptableObject.CreateInstance<Track> ();
				CyberBeat.MusicInfo music = new CyberBeat.MusicInfo ();
				music.AlbumImage = Tools.GetAssetAtPath<Sprite> ("Assets/Sprites/UI/AlbumPhoto/{0}.png".AsFormat (clip.name));
				music.clip = clip;
				var names = clip.name.Split ('-');
				music.AuthorName = names[0].Trim ();
				music.TrackName = names[1].Trim ();
				track.music = music;

				var koreography = ScriptableObject.CreateInstance<Koreography> ();
				foreach (var layer in Enums.LayerTypes)
				{
					var koreographyTrack = ScriptableObject.CreateInstance<KoreographyTrack> ();
					koreographyTrack.EventID = layer.ToString ();
					koreographyTrack.CreateAsset ("Assets/Data/Koreography/{0}/Tracks/{1}_{0}_KoreographyTrack.asset".AsFormat (name, layer));
					koreography.AddTrack (koreographyTrack);
				}
				koreography.SourceClip = music.clip;
				koreography.CreateAsset ("Assets/Data/Koreography/{0}/{0}_Koreography.asset".AsFormat (name));

				track.koreography = koreography;
				
				track.CreateAsset ("Assets/Resources/Data/Tracks/{0}.asset".AsFormat (clip.name));
			}
		}
	}
}
