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
        [SerializeField] float DelayedInvokeEndTrackTime = 1f;

        public void OnBit (IBitData bitData)
        {
            float timeSlowStop = CurrentTrack.ValueFast.music.clip.length - bitData.StartTime;
            this.DelayAction (DelayedInvokeEndTrackTime, () =>
            {
                OnTrackEnd.Invoke (timeSlowStop);
            });
        }

    }
}
