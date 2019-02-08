using System.Collections.Generic;
namespace CyberBeat
{
    using UnityEngine.UI.Extensions;
    using UnityEngine.UI;
    using UnityEngine;
    using Text = TMPro.TextMeshProUGUI;
    using GameCore;

    using System.Collections.Generic;
    using System;

    using UnityEngine.Events;

    public class AuthorsViewCell : MonoBehaviour
    {
        [SerializeField] Image AlbumeImage;
        [SerializeField] Text AuthorName;
        [SerializeField] Text TrackName;
        [SerializeField] GameObject Play;
        [SerializeField] GameObject Pause;
        [SerializeField] Transform SocialButtonsParent;
        [SerializeField] SocialButton SocialButtonPrefab;
        [SerializeField] UnityEvent OnPlayEvent;
        [SerializeField] UnityEvent OnPauseEvent;

        bool playing
        {
            set { Play.SetActive (!value); Pause.SetActive (value); }
            get { return !Play.activeSelf & Pause.activeSelf; }
        }
        AudioClip clip;
        static Action<Audio> OnPlay;
        private void Awake ()
        {
            OnPlay += checkMe;
        }
        private void OnDisable ()
        {
            PauseMusic ();
        }
        private void OnDestroy ()
        {
            OnPlay -= checkMe;
        }
        private void checkMe (Audio audio)
        {
            if (!clip) return;
            playing =
                audio != null &&
                audio.clip != null &&
                clip &&
                audio.clip
                .Equals (clip);

            if (!playing)
            {
                if (myAuido != null && myAuido.playing)
                {
                    myAuido.Pause ();
                    CancelInvoke ("PauseMusic");
                }

            }
        }

        AuthorsContext context;
        private int ID;
        public Audio myAuido;

        public void SetContext (AuthorsContext context)
        {
            this.context = context;
        }

        public void OnPressedCell ()
        {
            if (context != null)
            {
                context.OnPressedCell (this);
            }
        }
        MusicInfo music;
        public void UpdateContent (Track track)
        {
            music = track.music;
            clip = music.clip;
            name = track.name;
            AlbumeImage.sprite = music.AlbumImage;
            TrackName.text = music.TrackName;
            AuthorName.text = music.AuthorName;

            if (SocialButtonsParent.childCount == 0)
                InitSocialButtons (track.socials);
        }

        private void InitSocialButtons (List<SocialInfo> socials)
        {
            foreach (var social in socials)
            {
                var socButton = Instantiate (SocialButtonPrefab, SocialButtonsParent);
                socButton.Init (social);
            }

        }

        public void PlayMusic ()
        {
            if (myAuido != null && myAuido.paused)
                myAuido.Resume ();
            else
            {
                int ID = SoundManager.PlaySound (clip);
                myAuido = SoundManager.GetAudio (ID);
                myAuido.audioSource.time = music.StartPreviewSecond;
                Invoke ("PauseMusic", clip.length);
            }
            playing = true;
            if (OnPlay != null)
                OnPlay (myAuido);

            OnPlayEvent.Invoke ();
        }
        public void PauseMusic ()
        {
            playing = false;
            CancelInvoke ("PauseMusic");
            if (myAuido != null && myAuido.playing)
                myAuido.Pause ();

            OnPauseEvent.Invoke ();

        }

        public void TogglePlayPause ()
        {
            if (playing)
            {
                PauseMusic ();
            }
            else
            {
                PlayMusic ();
            }
        }
    }
}
