using GameCore;

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
		public List<SocialInfo> socials;
		public ShopInfo shopInfo;
		public TracksCollection data { get { return TracksCollection.instance; } }

		public int TrackNumber { get { return data.Objects.IndexOf (this) + 1; } }

		[ContextMenu ("Save ME")]
		public void SaveME ()
		{
			this.Save ();
		}

		public void ResetDefault ()
		{
			shopInfo.ResetDefault ();
		}

		public float StartSpeed = 50f;
		[ContextMenu ("Set Me As Current")]
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

		[ContextMenu ("Generate Random Playeble")]
		public void GenerateRandomPlayebles ()
		{
			var events = GetAllEventsByType (LayerType.Bit);
			var keys = data.Presets.Keys.ToList ();
			foreach (var evnt in events)
			{
				evnt.Payload = new IntPayload () { IntVal = keys.GetRandom () };
			}
			events.First ().Payload = new IntPayload () { IntVal = 0 };
		}

		[ContextMenu ("Generate BitInfo by Events")]
		public void GenerateBitInfoByEvnts ()
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
#if UNITY_EDITOR

		public LayerType LayerForGenerateTimEvents = LayerType.Combo;
		[ContextMenu ("Generate Combo TimeEvent`s")]
		void GenerateComboTimeEvents ()
		{
			var timeEventOfData = Tools.GetAssetAtPath<TimeOfEventsData> ("Assets/Data/TimeEvents/{0}/{1}.asset".AsFormat (name, LayerForGenerateTimEvents));
			timeEventOfData.Init (koreography.SampleRate, GetAllEventsByType (LayerForGenerateTimEvents));
		}
#endif

		public bool GetGateState (int index)
		{
			return Tools.GetBool ("{0} Gate {1}".AsFormat (name, index), true);
		}
		public void SetGateState (int index, bool value = true)
		{
			Tools.SetBool ("{0} Gate {1}".AsFormat (name, index), value);
		}
	}
}
