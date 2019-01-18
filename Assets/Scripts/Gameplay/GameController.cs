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
        [SerializeField] GameEvent resume;

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
                pause.Raise ();
        }

        void OnApplicationFocus (bool hasFocus)
        {
            isPaused = !hasFocus;
            if(!isPaused) resume.Raise();
        }

        void OnApplicationPause (bool pauseStatus)
        {
            isPaused = pauseStatus;
        }
#endif
    }
}
