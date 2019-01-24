using GameCore;

using System;
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

		[ContextMenu ("Validate ProgressInfo")]
		void ValidateProgressInfo ()
		{
			foreach (var track in Objects)
			{
				track.ValidateProgressInfo ();
			}
		}
#endif
		[SerializeField] LayerTypeABitDataCollectionVariableSelector CollectionSelector;
		[SerializeField] TrackVariable currentTrack;
		public Track CurrentTrack { get { return currentTrack.Value; } set { currentTrack.Value = value; } }

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

		public void UpdateCollections (LayerTypeTrackBitsCollectionSelector layerBitsSelector)
		{
			foreach (var layer in CollectionSelector.Keys)
			{
				var collection = layerBitsSelector[layer];
				CollectionSelector[layer].Value = collection;
			}
		}
	}
}
