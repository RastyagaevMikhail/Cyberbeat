using FluffyUnderware.Curvy.Controllers;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class GameController : MonoBehaviour
    {

        
        
        [SerializeField] GameEvent StartGame;
        //Raise On AppliacrtionPause or Focus
<<<<<<< HEAD
        [SerializeField] GameEvent pause;
        [SerializeField] GameEvent resume;
=======
        [SerializeField] GameEvent Pause;

        Player player { get { return Player.instance; } }
        GameData gameData { get { return GameData.instance; } }
        Track track { get { return gameData.currentTrack; } }
        BoostersData boostersData { get { return BoostersData.instance; } }
>>>>>>> parent of 46a173b... Fix Some Problem with Booster and Memory

        private bool gameStarted;

        void Awake ()
        {
            
            gameData.ResetCurrentProgress ();
        }

        public void OnDeathColorIterracble (ColorInterractor colorInterractor)
        {
            gameData.OnDestroyedBrick ();
        }
    
        public void OnPlayerContactWith (ColorInterractor interractor)
        {
            if (!interractor) return;

            ColorBrick colorBrick = interractor as ColorBrick;
            // Debug.Log ("{0} colorBrick = {1}".AsFormat (interractor, colorBrick));
            // Debug.LogFormat ("HasActiveBoosters = {0}", boostersData.HasActiveBoosters);
            if (colorBrick && boostersData.HasActiveBoosters)
                boostersData.ActivateBoosters (colorBrick);
            else
                interractor.OnPlayerContact ();
        }

        public void ToMenu ()
        {
            LoadingManager.instance.LoadScene ("Menu");
        }

        public void RestartGame ()
        {
            gameData.ResetCurrentProgress ();
            ReloadScene ();
        }

        public void ReloadScene ()
        {
            LoadingManager.instance.ReloadScene ();

        }

        public void OnFadeOut ()
        {
            if (!gameStarted)
            {
                gameStarted = true;
                StartGame.Raise ();
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
