using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [RequireComponent (typeof (AudioSource))]
    public class AudioVolumeChangeByTime : MonoBehaviour
    {
        private AudioSource _ASource = null;
        public AudioSource ASource { get { if (_ASource == null) _ASource = GetComponent<AudioSource> (); return _ASource; } }

        [SerializeField] float deltaTime = 1.5f;
        [SerializeField] float targetVolume = 1f;
        public void PlayFX ()
        {
            float targetTime = ASource.time - deltaTime;
            ASource.time = targetTime.Abs ();
            ASource.volume = 0;
            DOVirtual
                .Float (ASource.volume, targetVolume, deltaTime, value => ASource.volume = value)
                .SetEase (Ease.InQuint);
        }
    }
}
