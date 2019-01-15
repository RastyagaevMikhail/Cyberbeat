using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{

    [RequireComponent (typeof (AudioSource))]
    public class AudioEvents : MonoBehaviour
    {
        [SerializeField] UnityEventFloat OnStartPlay;
        [SerializeField] UnityEvent OnComplete;
        private AudioSource _aSource = null;
        public AudioSource aSource { get { if (_aSource == null) _aSource = GetComponent<AudioSource> (); return _aSource; } }
        public void Play (AudioClip clip = null)
        {
            if (!clip)
            {
                Debug.LogError ("clip is Null", this);
                return;
            }

            aSource.clip = clip;

            float length = clip.length;

            OnStartPlay.Invoke (length);

            this.DelayAction (length + Time.deltaTime, OnComplete.Invoke);
            aSource.time = 0f;
            aSource.Play ();
        }
    }
}
