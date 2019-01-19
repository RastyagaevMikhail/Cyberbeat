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
		#region EdiotHelpersFunctions

#if UNITY_EDITOR
		[ContextMenu ("Validate")]
		void Validate ()
		{
			ValidateMusicInfo ();
			ValidateShopInfo ();
			ValidateProgressInfo ();
			// CalculateConstant ();
			ValidateKoreography ();
			SetMeAsCurrent ();

			data.Objects.Add (this);
			this.Save ();
			UnityEditor.Selection.activeObject = this;
		}

		[ContextMenu ("Validate MusicInfo")]
		public void ValidateMusicInfo ()
		{
			music.Validate (name);
			this.Save ();
		}

		[ContextMenu ("Validate ShopInfo")]
		public void ValidateShopInfo ()
		{
			shopInfo.Validate (name);
			this.Save ();
		}

<<<<<<< HEAD
		[ContextMenu ("Validate ProgressInfo")]
		public void ValidateProgressInfo ()
=======
#if UNITY_EDITOR
		public void GenerateProgressInfo ()
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory
		{
			progressInfo.Generate (name);
			this.Save ();
		}

		[ContextMenu ("Validate Koreography")]
		private void ValidateKoreography ()
		{
			Koreography = Tools.ValidateSO<Koreography> ("Assets/Data/Koreography/{0}/{0}_Koreography.asset".AsFormat (name));
			Koreography.GetTempoSectionAtIndex (0).SectionName = "Default Selection";
			Koreography.InsertTempoSectionAtIndex (0).SectionName = "Zero Selection";
			Koreography.SourceClip = music.clip;
			ValidateKoreographyTracksLayers ();
		}

<<<<<<< HEAD
		[ContextMenu ("Validate Layers")]
		void ValidateKoreographyTracksLayers ()
		{
			foreach (var layer in Enums.LayerTypes)
			{
				var trackLayer = Tools.ValidateSO<KoreographyTrack> (("Assets/Data/Koreography/{0}/Tracks/{1}_{0}.asset").AsFormat (name, layer));
				UnityEditor.EditorUtility.SetDirty(trackLayer);
				trackLayer.EventID = layer.ToString ();
				if (Koreography.CanAddTrack (trackLayer))
					Koreography.AddTrack (trackLayer);
			}
		}
=======
		public Dictionary<LayerType, List<KoreographyEvent>> layerevents = new Dictionary<LayerType, List<KoreographyEvent>> ();
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory

		[ContextMenu ("CalculateConstant")]
		void CalculateConstant ()
		{
<<<<<<< HEAD
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
=======
			get { return layerevents[layer]; }
		}
		public Koreography koreography;
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory

				if (isContainConstant) progressInfo.Max++;
			}
			progressInfo.Save ();
		}

		[ContextMenu ("Generate Random Playeble")]
		public void GenerateRandomPlayebles ()
		{
			var events = GetAllEventsByType (LayerType.Bit);
			var keys = data.Presets.Keys.ToList ();
			RandomStack<int> randStack = new RandomStack<int>(keys);
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
						time = (float) e.StartSample / (float) Koreography.SampleRate,
				});
			}
		}

<<<<<<< HEAD
		[ContextMenu ("Convert To Text")]
		void ConvertToText ()
		{
			UnityEditor.EditorUtility.SetDirty (GetTrack (LayerType.Bit));
			foreach (var e in this [LayerType.Bit])
=======
		public LayerType LayerForGenerateTimEvents = LayerType.Combo;
		[ContextMenu ("Generate Combo TimeEvent`s")]
		void GenerateComboTimeEvents ()
		{
			var timeEventOfData = Tools.GetAssetAtPath<TimeOfEventsData> ("Assets/Data/TimeEvents/{0}/{1}.asset".AsFormat (name, LayerForGenerateTimEvents));
			timeEventOfData.Init (koreography.SampleRate, GetAllEventsByType (LayerForGenerateTimEvents));
		}

		[ContextMenu ("CalculateConstant")]
		void CalculateConstant ()
		{
			progressInfo.Max.Value = 0;
			foreach (var bitInfo in BitsInfos)
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory
			{
				int pld = e.GetIntValue ();
				var payload = new TextPayload ();
				payload.TextVal = pld.ToString ();
				e.Payload = payload;
			}

		}

<<<<<<< HEAD
		[ContextMenu ("Fix Paylaod")]
		void FixPayload ()
		{
			UnityEditor.EditorUtility.SetDirty (GetTrack (LayerType.Bit));
			foreach (var e in this [LayerType.Bit])
			{
				string pld = e.GetTextValue ();
				int value = -1;
				int.TryParse (pld, out value);
				if (value == -1 || !data.Presets.Keys.Contains (value))
				{
					TextPayload textPayload = new TextPayload ();
					textPayload.TextVal = string.Format ("{0}", 1);
					e.Payload = textPayload;
				}
			}

		}

		[ContextMenu ("Save ME")]
		public void SaveME ()
		{
			this.Save ();
		}
#endif
		[ContextMenu ("Set Me As Current")]
		public void SetMeAsCurrent ()
		{
			data.CurrentTrack = this;
		}
		#endregion
		public MusicInfo music;
		public List<SocialInfo> socials;
		public ShopInfo shopInfo;
		public ProgressInfo progressInfo;
		public TracksCollection data { get { return TracksCollection.instance; } }

		public int TrackNumber { get { return data.Objects.IndexOf (this) + 1; } }

		public void ResetDefault ()
		{
			shopInfo.ResetDefault ();
		}
		public float StartSpeed = 50f;
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

		[SerializeField] Koreography koreography;
		public Koreography Koreography { get { return koreography; } set { koreography = value; } }

		public List<KoreographyEvent> this [LayerType layer]
		{
			get
			{
				List<KoreographyEvent> events = null;
				layerevents.TryGetValue (layer, out events);
				return events;
			}
		}

		public KoreographyTrack GetTrack (LayerType layer)
		{
			return Koreography.GetTrackByID (layer.ToString ());
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
=======
				if (isContainConstant) progressInfo.Max.Increment ();
			}
			progressInfo.Max.Save();
		}
#endif
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory

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
