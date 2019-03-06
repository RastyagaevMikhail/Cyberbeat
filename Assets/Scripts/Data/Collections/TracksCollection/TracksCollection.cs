using GameCore;

using Sirenix.OdinInspector;

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

		[SerializeField] List<Preset> _presets;
		Dictionary<string, List<Material>> presets = null;
		public Dictionary<string, List<Material>> Presets
		{
			get
			{
				if (presets == null) presets = _presets.ToDictionary (p => p.Id, p => p.Objects);
				return presets;
			}
		}

		public void UpdateCollections (LayerTypeTrackBitsCollectionSelector layerBitsSelector)
		{
			// Debug.LogFormat ("CollectionSelector.Keys = {0}", CollectionSelector.Keys.Log ());
			// Debug.LogFormat ("CollectionSelector.Values = {0}", CollectionSelector.Values.Log ());
			foreach (var layer in CollectionSelector.Keys)
			{
				var collection = layerBitsSelector[layer];
				CollectionSelector[layer].Value = collection;
				CollectionSelector[layer].Save ();
			}
		}

		public bool CheckAsCurrent (Track track)
		{
			return currentTrack.ValueFast == track;
		}

		public void SetAsCurrent (Track track)
		{
			currentTrack.Value = track;
			currentTrack.Save ();
		}
#if UNITY_EDITOR
		[Button]
		public void SorByMaxConstants ()
		{
			Objects = Objects.OrderBy (track => track.progressInfo.Max).ToList ();
		}
		[Button] public void LogBalance() {
			string log =string.Empty;
			foreach (var item in Objects)
			{
				log += $"{item.name} {item.progressInfo.Max}\n";
			}
			Debug.Log(log);
		}

		[Button] public void CalculateMaxs ()
		{
			string log = string.Empty;
			foreach (var track in Objects)
			{
				log += $"{track.progressInfo.Max},";
			}
			Debug.Log (log);
		}

		[Button] public void CalculteTimes ()
		{
			string log = string.Empty;
			foreach (var track in Objects)
			{
				log += $"{(int)track.music.clip.length},";
			}
			Debug.Log (log);
		}
#endif
	}
}
