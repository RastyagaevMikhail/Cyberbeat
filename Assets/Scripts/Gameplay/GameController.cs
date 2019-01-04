using EZCameraShake;

using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
namespace CyberBeat
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance = null;
        public static GameController instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<GameController> (); return _instance; } }

        [SerializeField] IntVariable CurrentScore;
        [SerializeField] IntVariable BestScore;
        [SerializeField] GameEvent StartGame;
        [SerializeField] GameEvent Pause;
        [SerializeField] SplineController playerSplineController;
        [SerializeField] SplineController trackSplineController;
        Player player { get { return Player.instance; } }
        GameData gameData { get { return GameData.instance; } }
        Track track { get { return gameData.currentTrack; } }
        BoostersData boostersData { get { return BoostersData.instance; } }

        private bool gameStarted;

        void Awake ()
        {
            // playerSplineController.Speed = track.StartSpeed;
            // trackSplineController.Speed = track.StartSpeed;
            CurrentScore.Value = 0;
            gameData.ResetCurrentProgress ();
        }

        public void OnDeathColorIterracble (ColorInterractor colorInterractor)
        {
            gameData.OnDestroyedBrick ();
        }
        public void IncrementCurrentScore ()
        {
            gameData.Notes.Increment ();
            CurrentScore.Value++;
            if (CurrentScore.Value > BestScore.Value) BestScore.Value = CurrentScore.Value;
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
            CurrentScore.Value = 0;
            gameData.ResetCurrentProgress ();
            ReloadScene ();
        }

        public void ReloadScene ()
        {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
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
