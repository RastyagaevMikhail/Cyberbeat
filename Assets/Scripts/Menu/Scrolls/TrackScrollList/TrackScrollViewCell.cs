using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
namespace CyberBeat
{
	public class TrackScrollViewCell : MonoBehaviour
	{
		[SerializeField] Track track;
		[SerializeField] TrackPlayerCellView playerCellView;
		[SerializeField] GameObject PlayButton;
		[SerializeField] Image BuyButton;
		[SerializeField] ButtonActionByVideoAds PlayByWatchButton;
		[SerializeField] IntVariablesTextSetter progressTextSetter;
		[SerializeField] UnityEventGraphic onCantNotEnuthMoney;

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

			progressTextSetter.SetVariables (track.progressInfo.pregressVariables);
		}
		public void OnPlay ()
		{
			PlayButton.SetActive (false);
			track.SetMeAsCurrent ();
			track.LoadScene ();
		}

		public void OnPlayByWatch ()
		{
			Debug.Log ($"OnPlayByWatch {this}");
			ValidateButtons (true);
			OnPlay ();
		}

		public void OnBuy ()
		{
			bool buyed = track.shopInfo.TryBuy ();
			Debug.LogFormat ("buyed = {0}", buyed);
			ValidateButtons (buyed);
			if (buyed) OnPlay ();
			else onCantNotEnuthMoney.Invoke (BuyButton);
		}

		public void ValidateButtons (bool buyed)
		{
			PlayButton.SetActive (buyed);
			BuyButton.gameObject.SetActive (!buyed);
			PlayByWatchButton.gameObject.SetActive (!buyed);
		}
	}
}
