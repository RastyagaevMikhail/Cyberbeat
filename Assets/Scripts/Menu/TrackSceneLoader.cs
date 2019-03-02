using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class TrackSceneLoader : MonoBehaviour
    {
        [SerializeField] TrackVariable currentTrack;
        [SerializeField] LoadingManager loadingManager;
        public void LoadCurrentTrackScene ()
        {
            if (currentTrack.Value == null) return;
            loadingManager.LoadScene (currentTrack.Value.name);
        }
    }
}
