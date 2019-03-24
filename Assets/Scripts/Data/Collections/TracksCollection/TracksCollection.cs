using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Random = UnityEngine.Random;

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

		[ContextMenu ("CalculateBits")]
		void CalculateBits ()
		{
			var log = string.Empty;
			foreach (var track in Objects)
			{
				log += $"{track.name} : {track.progressInfo.Max} bts,{(int)track.music.clip.length} sec \n";
			}
			Debug.Log (log);

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
			return currentTrack.Value == track;
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

		[Button] public void LogBalance ()
		{
			string log = string.Empty;
			foreach (var item in Objects)
			{
				log += $"{item.name} {item.progressInfo.Max}\n";
			}
			Debug.Log (log);
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

		public int maxPrice = 5000;
		[ContextMenu ("ClearPrices")]
		void ClearPrices ()
		{
			foreach (var track in Objects)
			{
				track.shopInfo.Price = 0;
				track.Save ();
			}
		}

		[ShowInInspector] Dictionary<TrackDifficulty, List<Track>> DiffHash = new Dictionary<TrackDifficulty, List<Track>> ();
		[ContextMenu ("CalculateTrackPrices")]
		void CalculateTrackPrices ()
		{
			foreach (var track in Objects)
			{
				if (DiffHash.ContainsKey (track.difficulty))
					DiffHash[track.difficulty].Add (track);
				else
					DiffHash.Add (track.difficulty, new List<Track> () { track });
			}
			//  DiffHash.OrderBy (k => k.Key.minRate);
			float prevDiff = 0;
			foreach (var diff in DiffHash)
			{
				var sortedbits = diff.Value.OrderBy (t => t.progressInfo.Max);
				var emptyPrices = sortedbits.ToList ().FindAll (st => st.shopInfo.Price == 0);
				var count = emptyPrices.Count;
				var dif = prevDiff + (diff.Key.maxRate - diff.Key.minRate) * maxPrice;
				prevDiff = dif;
				var step = dif / count;
				int i = 1;
				foreach (var track in emptyPrices)
				{
					float rawPrice = step * i;
					int rawPrice1 = (int) rawPrice;
					int Price = rawPrice1 + (5 - rawPrice1 % 5);
					track.shopInfo.Price = Price;
					Debug.Log ($"{diff.Key.name} : {track.name} Price: {Price}");
					track.Save ();
					i++;
				}

			}
			/* for (int i = 2; i < Objects.Count; i++)
			{
				var track = Objects[i];
				TrackDifficulty dif = track.difficulty;
				int rawPrice = (int) (maxPrice * Random.Range (dif.minRate, dif.maxRate));
				Debug.Log ($"{track.name} rawPrice: {rawPrice}");
				int Price = rawPrice + (5 - rawPrice % 5);
				Debug.Log ($"{track.name} Price: {Price}");

				track.shopInfo.Price = Price;
				track.Save ();
			} */
		}
		public int startPrice = 50;
		public int startReward = 25;

		[ContextMenu ("CalculatePrices")]
		void CalculatePrices ()
		{
			int currentPrice = startPrice;
			int currentReward = startReward;
			string log = string.Empty;
			int index = 0;

			foreach (var track in Objects)
			{
				log += $"{track.name} : {currentPrice} {currentReward}\n";
				// track.shopInfo.Price = currentPrice;
				// track.maxReward = currentReward;
				// track.Save ();
				int rewardRaw = (int) currentReward * Multiplayers[index];
				currentPrice = rewardRaw + (5 - rewardRaw % 5);
				int rawReward = (int) currentPrice / 2;
				currentReward = rawReward - (rawReward % 5);
				index++;
			}
			Debug.Log (log);
		}
		public List<int> Multiplayers;
		[ContextMenu ("GenerateFib")]
		private void GenerateFib ()
		{
			Func<int, int> fib = null;
			fib = (x) => x > 1 ? fib (x - 1) + fib (x - 2) : x;
			Multiplayers = Enumerable.Range (0, 11).Select (x => fib (x)).ToList ();
		}
	}
}
