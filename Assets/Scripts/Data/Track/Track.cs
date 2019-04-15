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

		[ContextMenu ("Validate VideoInfo")]
		public void ValidateVideoInfo ()
		{
			videoInfo.Validate (name);
			this.Save ();
		}
		public void CalculateConstantOn (LayerType layer)
		{
			TrackBitsCollection trackBitsCollection = layerBitsSelector[layer];
			List<TrackBit> bits = trackBitsCollection.Bits.ToList ();
			int constants = helper.CalculateConstant (this, bits);
			progressInfo.Validate (name, constants);
		}

		[ContextMenu ("ValidateLayerBits")]
		public void ValidateLayerBitsSelector ()
		{
			layerBitsSelector = helper.ValidateLayerBitsSelector (this, layerBitsSelector);
			this.Save ();
		}
		public void ValidatePrefab ()
		{
			prefab = Tools.GetAssetAtPath<GameObject> ($"Assets/Prefabs/Tracks/{name}.prefab");
			this.Save ();
		}
		public void ValidateKoreography ()
		{
			koreography = helper.ValidateKoreography (this);
			this.Save ();
		}
		public void ValidateKoreographyTrackLayer ()
		{
			helper.ValidateKoreographyTrackLayer (koreography);
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
		TrackHelper helper => Resources.Load<TrackHelper> ("Data/TrackHelper");
#endif

		[HideIf ("IsCurrentTrack")]
		[Button ("Сделать меня текущим ", ButtonSizes.Medium)]
		[PropertyOrder (int.MaxValue)]
		[ContextMenu ("Set Me As Current")]
		public void SetMeAsCurrent ()
		{
			data.SetAsCurrent (this);
			data.UpdateCollections (layerBitsSelector);
		}
		#endregion
		public MusicInfo music;
		public List<SocialInfo> socials;
		public ShopInfo shopInfo;
		[InlineButton ("ValidateVideoInfo", "Validate")]
		public VideoInfo videoInfo;
		public int maxReward;
		[InlineButton ("ValidateProgressInfo", "Validate")]
		public ProgressInfo progressInfo;
		public float tutorialTime;
		[InlineButton ("ValidatePrefab", "Validate")]
		public GameObject prefab;
		public TrackDifficulty difficulty;
		public TrackStatus status;
		public Sprite nameSprite;
		[InlineButton ("ValidateLayerBitsSelector", "Valiadate")]
		[SerializeField] LayerTypeTrackBitsCollectionSelector layerBitsSelector;
		[InlineButton ("ValidateKoreographyTrackLayer", "L")]
		[InlineButton ("ValidateKoreography", "K")]
		[SerializeField] Koreography koreography;
		public float StartSpeed = 50f;
		public TracksCollection data => Resources.Load<TracksCollection> ("Data/TracksCollection");
		public bool IsCurrentTrack { get { return data.CheckAsCurrent (this); } }
		public int TrackNumber { get { return data.Objects.IndexOf (this) + 1; } }

		public KoreographyTrack GetTrack (LayerType layer)
		{
			return koreography.GetTrackByID (layer.name);
		}
		public List<KoreographyEvent> GetAllEventsByType (LayerType layer)
		{
			return GetTrack (layer).GetAllEvents ();
		}
		public void ResetDefault ()
		{
			shopInfo.ResetDefault ();
		}
	}
}
