using GameCore;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
	[ExecuteInEditMode]
	public class TrackScrollViewCell : MonoBehaviour
	{
		[SerializeField] Track track;
		[SerializeField] TrackPlayerCellView playerCellView;
		[SerializeField] LocalizeTextMeshProUGUI difficultyText;
		[SerializeField] Image statusImage;
		[SerializeField] Text MaxRedardValueText;
		[SerializeField] ContentButton BuyButton;
		[SerializeField] GameObject PlayButton;
		[SerializeField] GameObject PlayByWatchButton;
		[SerializeField] GameObject AdsButtonValidator;
		[SerializeField] UnityEvent onBuyed;
		[SerializeField] UnityEventGraphic onCantNotEnuthMoney;

		SystemLanguage currentLanguage => LocalizationManager.instance.currentLanguage;

		public void UpdateContent (Track data)
		{
			this.track = data;
			name = track.name;
			playerCellView.UpdateContent (data);
			ValidateTrackValues ();
			ValidateButtons (track.shopInfo.Buyed);
		}

		[ContextMenu ("Validate Track Values")]
		private void ValidateTrackValues ()
		{
			if (track == null) return;

			difficultyText.SetID (track.difficulty.LocalizationID);

			statusImage.enabled = track.status; // Если не указан статус то выключается
			BuyButton.text = track.shopInfo.Price.ToString ();

			if (track.status)
				statusImage.sprite = track.status.Sprite;

			MaxRedardValueText.text = track.maxReward.ToString ("### ### ##0");
		}
#if UNITY_EDITOR
		private void Update ()
		{
			ValidateTrackValues ();
		}
#endif

		public void SetAsCurrent ()
		{
			track.SetMeAsCurrent ();
		}
		public void OnBuy ()
		{
			bool buyed = track.shopInfo.TryBuy ();
			// Debug.LogFormat ("buyed = {0}", buyed);
			ValidateButtons (buyed);
			if (buyed) onBuyed.Invoke ();
			else onCantNotEnuthMoney.Invoke (BuyButton.targetGraphic);
		}
		public void ValidateButtons (bool buyed)
		{
			PlayButton.SetActive (buyed);
			BuyButton.SetActive (!buyed);

			VideoInfo videoInfo = track.videoInfo;
			videoInfo.CheckAcalivable ();

			int VideoCount = videoInfo.VideoCount;

			bool HasVideo = VideoCount != 0;

			bool VideoIsAvaliavable = !buyed && HasVideo;

			PlayByWatchButton.SetActive (VideoIsAvaliavable);
			AdsButtonValidator.SetActive (VideoIsAvaliavable);
		}
		public void OnVideoWatched ()
		{
			track.videoInfo.VideoCount--;
		}
	}
}
