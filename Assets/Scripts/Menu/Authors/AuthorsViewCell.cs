namespace CyberBeat
{
    using UnityEngine.UI.Extensions;
    using UnityEngine.UI;
    using UnityEngine;
    using Text = TMPro.TextMeshProUGUI;
    using GameCore;

    using System;

    public class AuthorsViewCell : FancyScrollViewCell<AuthorsData, AuthorsContext>
    {
        [SerializeField] Image AlbumeImage;
        [SerializeField] Text AuthorName;
        [SerializeField] Text TrackName;
        [SerializeField] GameObject Play;
        [SerializeField] GameObject Pause;
        [SerializeField] Image SocialButton;
        private string socilaURL;

        bool playing { set { Play.SetActive (!value); Pause.SetActive (value); } get { return !Play.activeSelf & Pause.activeSelf; } }
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
        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }

        readonly int scrollTriggerHash = Animator.StringToHash ("Scroll");
        AuthorsContext context;
        private int ID;
        public Audio myAuido;

        public override void SetContext (AuthorsContext context)
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
        public override void UpdateContent (AuthorsData data)
        {
            clip = data.track.clip;
            name = data.track.name;
            AlbumeImage.sprite = data.track.AlbulImage;
            TrackName.text = data.track.TrackName;
            AuthorName.text = data.track.AuthorName;

            SocialButton.gameObject.SetActive (data.track.SocialIcon);
            SocialButton.sprite = data.track.SocialIcon;
            socilaURL = data.track.SocialURL;
        }

        public void PlayMusic ()
        {
            if (myAuido != null && myAuido.paused)
                myAuido.Resume ();
            else
            {
                int ID = SoundManager.PlayMusic (clip);
                myAuido = SoundManager.GetAudio (ID);
                Invoke ("PauseMusic", clip.length);
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
                PauseMusic ();
            }
            else
            {
                PlayMusic ();
            }
        }

        public void OpenSocial ()
        {
            Application.OpenURL (socilaURL);
        }
    }
}
