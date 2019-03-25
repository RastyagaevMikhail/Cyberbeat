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
		[SerializeField] Text maxRewardValue;
		[SerializeField] ContentButton BuyButton;
		[SerializeField] GameObject PlayButton;
		[SerializeField] ContentButton PlayByWatchButton;
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

			maxRewardValue.text = track.maxReward.ToString ();
		}

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

			track.videoInfo.CheckAvalivable ();
			int videoCount = track.videoInfo.VideoCount;

			bool isAvaliavableVideo = videoCount != 0 && !buyed;

			PlayByWatchButton.SetActive (isAvaliavableVideo);
			AdsButtonValidator.SetActive (isAvaliavableVideo);

		}
		public void OnVideoWatched ()
		{
			track.videoInfo.VideoCount--;
		}

		private void Update ()
		{
			if (track)
			{
				ValidateTrackValues ();
			}
		}
	}
}
