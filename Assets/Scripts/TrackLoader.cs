using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class TrackLoader : MonoBehaviour
    {
        [SerializeField] TrackVariable trackVariable;
        [SerializeField] AudioSource audioSource;
        private void OnEnable ()
        {
            audioSource.clip = trackVariable.ValueFast.music.clip;
        }
    }
}
