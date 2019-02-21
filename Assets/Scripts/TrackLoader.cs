﻿using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class TrackLoader : MonoBehaviour
    {
        private const float timeShift = 1.5f;
        [SerializeField] TrackVariable trackVariable;
        [SerializeField] FloatVariable SpeedSplineVariable;
        [SerializeField] AudioSource audioSource;

        public void LoadStartSpeedToVariable ()
        {
            SpeedSplineVariable.ValueFast = trackVariable.ValueFast.StartSpeed;
        }

        public void LoadAudioClip ()
        {
            audioSource.clip = trackVariable.ValueFast.music.clip;
        }

        public void OnResumeTrack ()
        {
            if (audioSource.time - timeShift <= 0) return;
            audioSource.time -= timeShift;
            audioSource.volume = 0;

            audioSource.DOFade (1f, timeShift).SetEase (Ease.InExpo);
            audioSource.Play ();
        }
    }
}
