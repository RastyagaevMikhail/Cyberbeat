using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
namespace CyberBeat
{
	public class TrackScrollViewCell : FancyScrollViewCell<TrackScrollData, TrackScrollContext>
	{
		TracksCollection TracksCollection { get { return TracksCollection.instance; } }
		public LoadingManager loadManger { get { return LoadingManager.instance; } }

		[SerializeField] Track track;
		[SerializeField] Animator animator;
		[SerializeField] TrackPlayerCellView playerCellView;

		[SerializeField] Image Frame;
		[SerializeField] GameObject PlayButton;
		[SerializeField] GameObject BuyButton;
		[SerializeField] ButtonActionByVideoAds PlayByWatchButton;
		private void Awake ()
		{
			PlayByWatchButton.Init (OnPlayByWatch);
		}
		readonly int scrollTriggerHash = Animator.StringToHash ("Scroll");
		TrackScrollContext context;
        private bool WathedRewardVideo = false;

        public override void SetContext (TrackScrollContext context)
		{
			this.context = context;
		}

		public override void UpdatePosition (float position)
		{
			animator.Play (scrollTriggerHash, -1, position);
			animator.speed = 0;
		}

		public void OnPressedCell ()
		{
			if (context != null)
			{
				context.OnPressedCell (this);
			}
		}
		public override void UpdateContent (TrackScrollData data)
		{
			// Frame.sprite = data.Frame;

			this.track = data.track;
			name = track.name;
			playerCellView.UpdateContent (data);
			ValidateTrackValues ();
			ValidateButtons (track.shopInfo.Buyed || WathedRewardVideo);
			// UpdatePosition (0);
		}

		[Button]
		private void ValidateTrackValues ()
		{
			if (track == null) return;

			if (context == null) return;

			if (context.SelectedIndex == DataIndex)
				track.SetMeAsCurrent ();

		}
		public void OnPlay ()
		{
			track.SetMeAsCurrent ();
			track.LoadScene ();
		}

		public void OnPlayByWatch ()
		{
			Debug.Log ("OnPlayByWatch {0}".AsFormat(this));
			WathedRewardVideo = true;
			ValidateButtons (true);
		}

		public void OnBuy ()
		{
			bool buyed = track.shopInfo.TryBuy ();
			Debug.LogFormat ("buyed = {0}", buyed);
			ValidateButtons (buyed);
		}

		private void ValidateButtons (bool buyed)
		{
			// Debug.LogFormat ("ValidateButtons = {0}, {1}", buyed, this);
			PlayButton.SetActive (buyed);
			BuyButton.SetActive (!buyed);
			PlayByWatchButton.gameObject.SetActive (!buyed);
		}
	}
}
