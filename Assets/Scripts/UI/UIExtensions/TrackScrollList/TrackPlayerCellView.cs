using System;
using System.Collections;
using System.Collections.Generic;



using TMPro;

using UnityEngine;
using UnityEngine.UI;

using GameCore;
namespace CyberBeat
{
	public class TrackPlayerCellView : MonoBehaviour
	{

		[SerializeField] Track track;
		[SerializeField] TextMeshProUGUI TrackName;
		[SerializeField] TextMeshProUGUI AuthorName;
		[SerializeField] TextMeshProUGUI TrackNumber;
		[SerializeField] Image Album;
		[SerializeField] GameObject Play;
		[SerializeField] GameObject Pause;
		bool playing { set { Play.SetActive (!value); Pause.SetActive (value); } get { return !Play.activeSelf & Pause.activeSelf; } }
		static Action<Audio> OnPlay;
		private void Awake ()
		{
			OnPlay += checkMe;
		}

		private void OnDestroy ()
		{
			OnPlay -= checkMe;
		}

		private void checkMe (Audio audio)
		{
			if (!track) return;
			playing =
				audio != null &&
				audio.clip != null &&
				track &&
				track.music.clip &&
				audio.clip
				.Equals (track
					.music.clip);

			if (!playing)
			{
				if (myAuido != null && myAuido.playing)
				{
					myAuido.Pause ();
					CancelInvoke ("PauseMusic");
				}

			}
		}

		public void UpdateContent (TrackScrollData data)
		{
			track = data.track;
			// AuthorName.color = data.color;

			TrackName.text = track.music.TrackName;
			AuthorName.text = track.music.AuthorName;
			TrackNumber.text = track.TrackNumber.ToString ();
			Album.sprite = track.music.AlbumImage;
		}
		Audio myAuido;
		public void PlayMusic ()
		{
			if (myAuido != null && myAuido.paused)
				myAuido.Resume ();
			else
			{
				int ID = SoundManager.PlayMusic (track.music.clip);
				myAuido = SoundManager.GetAudio (ID);
				Invoke ("PauseMusic", track.music.clip.length);
			}
			playing = true;
			if (OnPlay != null)
				OnPlay (myAuido);
		}
		public void PauseMusic ()
		{
			playing = false;
			CancelInvoke ("PauseMusic");
			if (myAuido != null && myAuido.playing)
				myAuido.Pause ();

		}

		public void TogglePlayPause ()
		{
			if (playing)
			{
				PauseMusic();
			}
			else
			{
				PlayMusic ();
			}
		}
	}
}
