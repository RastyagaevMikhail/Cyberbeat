using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
namespace CyberBeat.Editor
{
	using GameCore;

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
				track.AlbulImage = Tools.GetAssetAtPath<Sprite> ("Assets/Sprites/UI/AlbumPhoto/{0}.png".AsFormat (clip.name));
				track.clip = clip;
				var names = clip.name.Split ('-');
				track.AuthorName = names[0].Trim ();
				track.TrackName = names[1].Trim ();
				track.CreateAsset ("Assets/Resources/Data/Tracks/{0}.asset".AsFormat (clip.name));
			}
		}
	}
}
