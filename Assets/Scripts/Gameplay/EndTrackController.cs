using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class EndTrackController : MonoBehaviour
    {
        [SerializeField] TrackVariable CurrentTrack;
        [SerializeField] UnityEventFloat OnTrackEnd;

        public void OnBit (IBitData bitData)
        {
            float timeSlowStop = CurrentTrack.Value.music.clip.length - bitData.StartTime;
            OnTrackEnd.Invoke (timeSlowStop);
        }

    }
}
