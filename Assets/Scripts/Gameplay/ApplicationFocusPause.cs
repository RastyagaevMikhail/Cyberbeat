using FluffyUnderware.Curvy.Controllers;

using GameCore;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class ApplicationFocusPause : MonoBehaviour
    {
        [SerializeField] bool enableDetection;
        [SerializeField] UnityEvent pause;
        [SerializeField] UnityEvent resume;
        public bool EnableDetection
#if UNITY_EDITOR
            => enableDetection;
#else 
        => true;
#endif
        bool sceneEnabled = false;
        private void Start ()
        {
            sceneEnabled = true;
        }
        private void OnDisable ()
        {
            sceneEnabled = false;
        }
        bool isPaused = false;

        void Update ()
        {
            if (!EnableDetection) return;
            if (!sceneEnabled) return;
            if (isPaused)
                pause.Invoke ();
        }

        void OnApplicationFocus (bool hasFocus)
        {
            if (!EnableDetection) return;
            if (!sceneEnabled) return;
            isPaused = !hasFocus;
            if (!isPaused) resume.Invoke ();
        }

        void OnApplicationPause (bool pauseStatus)
        {
            if (!EnableDetection) return;
            if (!sceneEnabled) return;
            isPaused = pauseStatus;
        }
    }
}
