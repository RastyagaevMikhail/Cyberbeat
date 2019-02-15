﻿using GameCore;

using Sirenix.OdinInspector;

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

		[ContextMenu ("Validate ProgressInfo")]
		public void ValidateProgressInfo ()
		{
			progressInfo.Validate (name);
			this.Save ();
		}

		[ContextMenu ("Validate Koreography")]
		public void ValidateKoreography ()
		{
			Koreography = Tools.ValidateSO<Koreography> ("Assets/Data/Koreography/{0}/{0}_Koreography.asset".AsFormat (name));
			Koreography.GetTempoSectionAtIndex (0).SectionName = "Default Selection";
			Koreography.InsertTempoSectionAtIndex (0).SectionName = "Zero Selection";
			Koreography.SourceClip = music.clip;
			ValidateKoreographyTracksLayers ();
		}

		[ContextMenu ("Validate Layers")]
		public void ValidateKoreographyTracksLayers ()
		{
			List<KoreographyTrack> tracks = Koreography.Tracks;
			Debug.LogFormat ("tracks = {0}", Tools.LogCollection(tracks));
			foreach (var track in tracks)
			{
				koreography.RemoveTrack(track);
			}
			tracks = Koreography.Tracks;
			Debug.LogFormat ("tracks = {0}", Tools.LogCollection(tracks));
			
			// tracks.Clear ();
			foreach (var layer in Enums.instance.LayerTypes)
			{
				var trackLayer = Tools.ValidateSO<KoreographyTrack> (("Assets/Data/Koreography/{0}/Tracks/{1}_{0}.asset").AsFormat (name, layer));
				UnityEditor.EditorUtility.SetDirty (trackLayer);
				trackLayer.EventID = layer.ToString ();
				// if (Koreography.CanAddTrack (trackLayer))
					Koreography.AddTrack (trackLayer);
			}
			Koreography.Save();
			SaveME();
		}
		[ContextMenu ("Save ME")]
		public void SaveME ()
		{
			this.Save ();
		}

		

		[OnInspectorGUI]
		[PropertyOrder (int.MaxValue - 1)]
		private void InfoGUI ()
		{
			if (IsCurrentTrack)
				Sirenix.Utilities.Editor.SirenixEditorGUI.InfoMessageBox ("ТЕКУЩИЙ ");
			else
				Sirenix.Utilities.Editor.SirenixEditorGUI.ErrorMessageBox ("НЕ ЯВЛЯЕТСЯ текущим ");
		}
#endif
		public bool IsCurrentTrack { get { return this == data.CurrentTrack; } }

		[HideIf ("IsCurrentTrack")]
		[Button ("Сделать меня текущим ", ButtonSizes.Medium)]
		[PropertyOrder (int.MaxValue)]
		[ContextMenu ("Set Me As Current")]
		public void SetMeAsCurrent ()
		{
			data.CurrentTrack = this;
			data.UpdateCollections (layerBitsSelector);
		}
		#endregion
		public MusicInfo music;
		public List<SocialInfo> socials;
		public ShopInfo shopInfo;
		[InlineButton ("ValidateProgressInfo", "Validate")]
		public ProgressInfo progressInfo;
		[InlineButton ("ValidateLayerBits", "Validate")]
		[ContextMenuItem ("Valiadate", "ValidateLayerBits")]
		[SerializeField] LayerTypeTrackBitsCollectionSelector layerBitsSelector;
#if UNITY_EDITOR
		[ContextMenu ("ValidateLayerBits")]
		public void ValidateLayerBits ()
		{
			layerBitsSelector = Tools.ValidateSO<LayerTypeTrackBitsCollectionSelector> ($"Assets/Data/Selectors/Tracks/{name}_LayerBitsSelector.asset");
			layerBitsSelector.datas = new List<LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData> ();
			foreach (var layer in Enums.instance.LayerTypes)
			{
				var dataBits = Tools.ValidateSO<TrackBitsCollection> ($"Assets/Data/Track{layer}sCollection/{name}_{layer}.asset");
				dataBits.Init (GetAllEventsByType (layer));

				layerBitsSelector.datas.Add (new LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData ()
				{
					type = layer,
						data = dataBits
				});
			}
			layerBitsSelector.Save ();
			this.Save ();
		}
#endif
		public TracksCollection data { get { return TracksCollection.instance; } }

		public int TrackNumber { get { return data.Objects.IndexOf (this) + 1; } }

		public void ResetDefault ()
		{
			shopInfo.ResetDefault ();
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
					(_layerevents = Enums.instance.LayerTypes
						.Select (layer => new { Layer = layer, Events = GetTrack (layer).GetAllEvents () })
						.ToDictionary (les => les.Layer, les => les.Events));
			}

		}

		[InlineButton ("ValidateKoreographyTracksLayers", "L")]
		[InlineButton ("ValidateKoreography", "K")]
		[SerializeField] Koreography koreography;
		public Koreography Koreography { get { return koreography; } set { koreography = value; } }

        public LayerTypeTrackBitsCollectionSelector LayerBitsSelector { get => layerBitsSelector; set => layerBitsSelector = value; }

        public float StartSpeed = 50f;

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
			return Koreography.GetTrackByID (layer.name);
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
