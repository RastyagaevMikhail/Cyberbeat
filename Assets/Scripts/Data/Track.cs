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
		public ProgressInfo progressInfo;
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

#if UNITY_EDITOR
		[ContextMenu ("Validate ProgressInfo")]
		public void ValidateProgressInfo ()
		{
			progressInfo.Validate (name);
			this.Save ();
		}
#endif

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

		Dictionary<LayerType, List<KoreographyEvent>> _layerevents = null;
		Dictionary<LayerType, List<KoreographyEvent>> layerevents
		{

			get
			{
				return _layerevents ??
					(_layerevents = Enums.LayerTypes
						.Select (layer => new { Layer = layer, Events = GetTrack (layer).GetAllEvents () })
						.ToDictionary (les => les.Layer, les => les.Events));
			}

		}

		public List<KoreographyEvent> this [LayerType layer]
		{
			get
			{
				List<KoreographyEvent> events = null;
				layerevents.TryGetValue (layer, out events);
				return events;
			}
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

		[ContextMenu ("Generate Random Playeble")]
		public void GenerateRandomPlayebles ()
		{
			var events = GetAllEventsByType (LayerType.Bit);
			var keys = data.Presets.Keys.ToList ();
			RandomStack<int> randStack = new RandomStack<int> (keys);
			foreach (var evnt in events)
			{
				evnt.Payload = new IntPayload () { IntVal = randStack.Get () };
			}
			events.First ().Payload = new IntPayload () { IntVal = 1 };
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

		[ContextMenu ("CalculateConstant")]
		void CalculateConstant ()
		{

			progressInfo.Max = 0;
			foreach (var bitInfo in BitsInfos)
			{
				List<int> presetList = bitInfo.presets.ToList ();

				bool isContainConstant = presetList
					.TrueForAll (p => data.Presets[p]
						.Find (spwnObj =>
						{
							if (spwnObj)
								return spwnObj.Get<MaterialSwitcher> ().Constant;
							return false;
						}));

				if (isContainConstant) progressInfo.Max++;
			}
			progressInfo.Save ();
		}

		[ContextMenu ("InitByMusic")]
		void InitByMusic ()
		{

			var SpitedName = name.Split ("-".ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
			this.Save ();
			music.AuthorName = SpitedName[0];
			music.TrackName = SpitedName[1];

			shopInfo.SaveKey = "{0} Buyed".AsFormat (name);

			progressInfo.Validate (name);
			var koreography = CreateInstance<Koreography> ();
			this.koreography = koreography;
			this.koreography.SourceClip = music.clip;
			koreography.GetTempoSectionAtIndex (0).SectionName = "Default Selection";
			koreography.InsertTempoSectionAtIndex (0).SectionName = "Zero Selection";
			this.koreography.CreateAsset (("Assets/Data/Koreography/{0}/{0}_Koreography.asset").AsFormat (name));
			foreach (var layer in Enums.LayerTypes)
			{
				var trackLayer = CreateInstance<KoreographyTrack> ();
				trackLayer.CreateAsset (("Assets/Data/Koreography/{0}/Traks/{1}_{0}.asset").AsFormat (name, layer));
				trackLayer.EventID = layer.ToString ();
				koreography.AddTrack (trackLayer);
			}
			CalculateConstant ();
			data.Objects.Add (this);
			SetMeAsCurrent ();
			UnityEditor.Selection.activeObject = this;
			this.Save ();
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
