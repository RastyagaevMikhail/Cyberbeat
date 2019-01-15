using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

	public class TracksCollection : DataCollections<TracksCollection, Track>
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/Collections/Tracks")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

		public override void InitOnCreate ()
		{
			Objects = Tools.GetAtPath<Track> ("Assets/Resources/Data/Tracks").ToList ();
		}
		public override void ResetDefault ()
		{
			foreach (var track in Objects)
			{
				track.ResetDefault ();
			}
		}

		[ContextMenu ("validate ProgressInfo")]
		void ValidateProgressInfo ()
		{
			foreach (var track in Objects)
			{
				track.ValidateProgressInfo ();
			}
		}
#endif

		public Track CurrentTrack;
		[SerializeField] List<Preset> _presets;
		Dictionary<int, List<SpawnedObject>> presets = null;
		public Dictionary<int, List<SpawnedObject>> Presets
		{
			get
			{
				if (presets == null) presets = _presets.ToDictionary (p => p.Id, p => p.Objects);
				return presets;
			}
		}

		[ContextMenu ("PrintKeys")]
		void PrintKeys ()
		{
			Debug.Log (Tools.LogCollection (Presets.Keys));
		}
	}
}
