using GameCore;

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
		public void CalculateConstantOn (LayerType layer)
		{
			TrackBitsCollection trackBitsCollection = layerBitsSelector[layer];
			List<IBitData> bits = trackBitsCollection.Bits;
			int constants = helper.CalculateConstant (this, bits);
			progressInfo.Validate (name, constants);
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
		public TrackHelper helper { get { return TrackHelper.instance; } }
#endif
		public bool IsCurrentTrack { get { return data.CheckAsCurrent(this); } }

		[HideIf ("IsCurrentTrack")]
		[Button ("Сделать меня текущим ", ButtonSizes.Medium)]
		[PropertyOrder (int.MaxValue)]
		[ContextMenu ("Set Me As Current")]
		public void SetMeAsCurrent ()
		{
			data.SetAsCurrent(this);
			data.UpdateCollections (layerBitsSelector);
		}
		#endregion
		public MusicInfo music;
		public List<SocialInfo> socials;
		public ShopInfo shopInfo;
		[InlineButton ("ValidateProgressInfo", "Validate")]
		public ProgressInfo progressInfo;
		[InlineButton ("ValidateLayerBitsSelector", "Valiadate")]
		[SerializeField] LayerTypeTrackBitsCollectionSelector layerBitsSelector;
#if UNITY_EDITOR
		[ContextMenu ("ValidateLayerBits")]
		public void ValidateLayerBitsSelector ()
		{
			helper.ValidateLayerBitsSelector (this, layerBitsSelector);
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

		[InlineButton ("ValidateKoreographyTrackLayer", "L")]
		[InlineButton ("ValidateKoreography", "K")]
		[SerializeField] Koreography koreography;
#if UNITY_EDITOR
		public void ValidateKoreography ()
		{
			koreography = helper.ValidateKoreography (this);
		}
		public void ValidateKoreographyTrackLayer ()
		{
			helper.ValidateKoreographyTrackLayer (koreography);
		}
#endif
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
			return koreography.GetTrackByID (layer.name);
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
