using FluffyUnderware.Curvy.Controllers;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] GameEvent startGame;
        //Raise On AppliacrtionPause or Focus
        [SerializeField] GameEvent pause;

        private bool gameStarted;

        public void OnFadeOut ()
        {
            if (!gameStarted)
            {
                gameStarted = true;
                startGame.Raise ();
            }
        }
        private void Start ()
        {
            var unusedTracks = TracksCollection.instance.Objects.FindAll (t => !t.Equals (TracksCollection.instance.CurrentTrack));
            foreach (var track in unusedTracks)
            {
                Resources.UnloadAsset (track);
            }
        }

#if !UNITY_EDITOR
        bool isPaused = false;

        void Update ()
        {
            if (isPaused)
                Pause.Raise ();
        }

        void OnApplicationFocus (bool hasFocus)
        {
            isPaused = !hasFocus;
        }

        void OnApplicationPause (bool pauseStatus)
        {
            isPaused = pauseStatus;
        }
#endif
    }
}
