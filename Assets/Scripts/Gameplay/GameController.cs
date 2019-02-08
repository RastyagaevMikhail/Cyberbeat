using FluffyUnderware.Curvy.Controllers;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] GameEvent pause;
        [SerializeField] GameEvent resume;
        private bool gameStarted;

#if !UNITY_EDITOR
        //Raise On AppliacrtionPause or Focus

        bool isPaused = false;

        void Update ()
        {
            if (isPaused)
                pause.Raise ();
        }

        void OnApplicationFocus (bool hasFocus)
        {
            isPaused = !hasFocus;
            if (!isPaused) resume.Raise ();
        }

        void OnApplicationPause (bool pauseStatus)
        {
            isPaused = pauseStatus;
        }
#endif
    }
}
