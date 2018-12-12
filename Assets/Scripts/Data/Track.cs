using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

using SonicBloom.Koreo;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	[CreateAssetMenu (fileName = "Track", menuName = "CyberBeat/Track", order = 0)]
	public class Track : ScriptableObject
	{
		public MusicInfo music;
		public SocialInfo social;
		[OdinSerialize]
		public ShopInfo shopInfo;
		public TracksCollection data { get { return TracksCollection.instance; } }

		public int TrackNumber { get { return data.Objects.IndexOf (this) + 1; } }

		[Button] public void SaveME ()
		{
			this.Save ();
		}
		public float StartSpeed = 50f;
		[Button]
		public void SetMeAsCurrent ()
		{
			data.CurrentTrack = this;
		}

		public void LoadScene ()
		{
			LoadingManager.instance.LoadScene (name);
		}

		public Dictionary<LayerType, List<KoreographyEvent>> layerevents = new Dictionary<LayerType, List<KoreographyEvent>> ();

		public List<KoreographyEvent> this [LayerType layer]
		{
			get { return layerevents[layer]; }
		}
		public Koreography koreography;

		public KoreographyTrack GetTrack (LayerType layer)
		{
			return koreography.GetTrackByID (layer.ToString ());
		}
		public List<KoreographyEvent> GetAllEventsByType (LayerType layer)
		{
			return GetTrack (layer).GetAllEvents ();
		}
		public List<KoreographyEvent> GetLongEventsByType (LayerType layer)
		{
			return GetAllEventsByType (layer).FindAll (e => e.EndSample - e.StartSample > 0);
		}
		public List<KoreographyEvent> GetShortEventsByType (LayerType layer)
		{
			return GetAllEventsByType (layer).FindAll (e => e.EndSample == e.StartSample);
		}

		public float MinTimeOfBit = 0.2f;
		public List<BitInfo> BitsInfos = new List<BitInfo> ();
		public int CountConstant;

		[Button] public void GenerateRandomPlayebles ()
		{
			var events = GetAllEventsByType (LayerType.Bit);
			var keys = data.Prefabs.Keys.ToList ();
			foreach (var evnt in events)
			{
				evnt.Payload = new IntPayload () { IntVal = keys.GetRandom () };
			}
		}

		[Button] public void GenerateBitInfoByEvnts ()
		{
			BitsInfos = new List<BitInfo> ();
			var events = GetAllEventsByType (LayerType.Bit);
			foreach (var e in events)
			{
				BitsInfos.Add (new BitInfo ()
				{
					presets = new int[] { e.GetIntValue () },
						time = (float) e.StartSample / (float) koreography.SampleRate,
				});
			}
		}
	}
}
