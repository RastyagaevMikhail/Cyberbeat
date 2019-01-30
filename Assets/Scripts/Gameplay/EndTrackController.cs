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
        [SerializeField] float timeFromEnd = float.MaxValue;
        [SerializeField] float DelayedInvokeEndTrackTime = 1f;

        private void Start ()
        {
            resetEnd ();
        }

        private void resetEnd ()
        {
            timeFromEnd = float.MaxValue;
        }

        public void OnTrackGeneratorEndGenerate (float time)
        {
            timeFromEnd = time;
        }
        public void OnUpdate (float time)
        {
            if (time >= timeFromEnd)
            {
                float timeSlowStop = CurrentTrack.ValueFast.music.clip.length - timeFromEnd;
                this.DelayAction (DelayedInvokeEndTrackTime, () =>
                {
                    OnTrackEnd.Invoke (timeSlowStop);
                });
                resetEnd ();
            }

        }
    }
}
