﻿using FluffyUnderware.Curvy;
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
        [SerializeField] SplineController playerSplineController;
        [SerializeField] SplineController trackSplineController;
        [SerializeField] List<EventListener> listeners;
        Player player { get { return Player.instance; } }
        GameData gameData { get { return GameData.instance; } }
        Track track { get { return gameData.currentTrack; } }
        BoostersData boostersData { get { return BoostersData.instance; } }

        [SerializeField] BoosterData currentBooster;
        private void OnEnable ()
        {
            listeners.ForEach (l => l.OnEnable ());
        }
        void Awake ()
        {
            playerSplineController.Speed = track.StartSpeed;
            trackSplineController.Speed = track.StartSpeed;
            CurrentScore.Value = 0;
            gameData.ResetCurrentProgress ();
        }
        private void OnDisable ()
        {
            listeners.ForEach (l => l.OnDisable ());
        }
        void Update ()
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                RestartGame ();
            }
        }
        public void OnDeathColorIterracble (UnityObjectVariable unityObject)
        {

        }
        public void IncrementCurrentScore ()
        {
            gameData.Notes.Increment ();
            CurrentScore.Value++;
            if (CurrentScore.Value > BestScore.Value) BestScore.Value = CurrentScore.Value;
        }
        public void OnPlayerContactWith (UnityObjectVariable unityObject)
        {
            Interractor interractor = null;
            if (!unityObject.CheckAs<Interractor> (out interractor)) return;
            ColorBrick colorBrick = interractor as ColorBrick;
            bool isBrick = colorBrick;
            bool isShieldActive = boostersData["Shield"].Equals (currentBooster);
            bool isOneLifeActive = boostersData["OneLife"].Equals (currentBooster);
            if (isBrick)
            {
                if (isShieldActive)
                {
                    colorBrick.Death ();
                }
                else if (isOneLifeActive)
                {
                    currentBooster.Reset ();
                    colorBrick.Death ();
                }
                else
                {
                    colorBrick.OnPlayerContact (interractor.gameObject);
                }
            }
        }

        public void OnBoosterStart (UnityObjectVariable unityObject)
        {
            if (!unityObject.CheckAs<BoosterData> (out currentBooster)) return;

            currentBooster.timer.OnTimeElapsed += OnBoosterTimeEnd;

        }

        private void OnBoosterTimeEnd ()
        {
            currentBooster.timer.OnTimeElapsed -= OnBoosterTimeEnd;
            currentBooster.timer = null;
            currentBooster = null;
        }
        public void Pause ()
        {
            Time.timeScale = 0;
        }
        public void Resume ()
        {
            Time.timeScale = 1;
        }
        public void ToMenu ()
        {
            Resume ();
            LoadingManager.instance.LoadScene ("Menu");
        }

        [SerializeField] float StopDuration = 2f;
        public void _RestartGameWithDelay ()
        {
            playerSplineController.Speed.Do (0f, StopDuration, value =>
            {
                playerSplineController.Speed = value;
                playerSplineController.Play ();
            });
            playerSplineController.Stop ();
            Invoke ("RestartGame", StopDuration);
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

    }
}
