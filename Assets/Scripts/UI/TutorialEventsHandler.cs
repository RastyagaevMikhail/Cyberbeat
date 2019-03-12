using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class TutorialEventsHandler : MonoBehaviour
    {
        [SerializeField] TrackVariable currentTrack;
        float tutorialTime => currentTrack.Value.tutorialTime;
        [SerializeField] UnityEventFloat startTutoirial;
        [SerializeField] UnityEvent OnTutorDisabled;
        public void DoCheck ()
        {
            if (tutorialTime == 0) { OnTutorDisabled.Invoke (); return; }

            startTutoirial.Invoke (tutorialTime);
        }

    }
}
