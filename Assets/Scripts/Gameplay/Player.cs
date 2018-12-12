using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    public class Player : Interractor
    {
        private static Player _instance = null;
        public static Player instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<Player> (); return _instance; } }
        private MaterialSwitcher _matSwitch;
        public MaterialSwitcher matSwitch { get { if (_matSwitch == null) _matSwitch = GetComponentInChildren<MaterialSwitcher> (); return _matSwitch; } }
        private ColorInterractor _colorInterractor = null;
        public ColorInterractor colorInterractor { get { if (_colorInterractor == null) _colorInterractor = GetComponentInChildren<ColorInterractor> (); return _colorInterractor; } }
        private TrailRenderer _trail = null;
        public TrailRenderer trail { get { if (_trail == null) _trail = GetComponentInChildren<TrailRenderer> (); return _trail; } }

        [SerializeField] InputControlType CurrentInputType;
        public InputSettings inputSettings;
        [SerializeField] Dictionary<InputControlType, IInputController> InputController;
        [SerializeField] GameEvent OnDeath;
        [SerializeField] GameEventObject OnColorChnaged;

        public void SetControl (InputControlType controlTypeToSwitch)
        {
            CurrentInputType = controlTypeToSwitch;
        }
        public void MoveRight ()
        {
            InputController[CurrentInputType].MoveRight ();
        }
        public void MoveLeft ()
        {
            InputController[CurrentInputType].MoveLeft ();
        }
        public void OnColorChangeOnInterracor (UnityObjectVariable unityObjectVariable)
        {
            // Debug.Log ("OnColorChangeOnInterracor");
            ColorVariable colorVariable = null;
            if (!unityObjectVariable.CheckAs<ColorVariable> (out colorVariable)) return;
            // Debug.LogFormat ("colorVariable = {0}", colorVariable);
            Color color = colorVariable.Value;
            // Debug.LogFormat ("color = {0}", color.ToString (false));
            // Debug.LogFormat ("matSwitch.CurrentColor = {0}", matSwitch.CurrentColor.ToString (false));
            if (!matSwitch.ChechColor (color))
            {
                OnColorChnaged.Raise (colorVariable);
                // Debug.Log ("OnColorChnaged");
            }
        }
        public void SetColor (Color color)
        {
            matSwitch.SetColorInMaterial (color);

            if (trail)
                trail.material.SetColor ("_EmissionColor", color);
        }
        GameData gameData { get { return GameData.instance; } }
        Track track { get { return gameData.currentTrack; } }
        // float minTimeOfBit { get { return track[gameData.CurrentDifficulty].MinTimeOfBit; } }
        // float startSpeed { get { return track[gameData.CurrentDifficulty].StartSpeed; } }
        private void Awake ()
        {
            SetColor (Color.white);

            inputSettings.SwipeDuration = 0.2f; /* minTimeOfBit */ ;
            foreach (var item in InputController)
                item.Value.Init (transform, inputSettings);
        }

        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }
        public void Death ()
        {
            colorInterractor.Death ();
            OnDeath.Raise ();
            gameObject.SetActive (false);
        }

    }
}
