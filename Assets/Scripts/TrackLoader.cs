using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class TrackLoader : MonoBehaviour
    {
        [SerializeField] TrackVariable trackVariable;
        [SerializeField] UnityEventAudioClip OnLoadAudioClip;

        public void LoadAuiodClip ()
        {
            OnLoadAudioClip.Invoke (trackVariable.Value.music.clip);
        }

    }
}
