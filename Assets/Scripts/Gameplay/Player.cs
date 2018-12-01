using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
    public class Player : ColorInterractor
    {
        private static Player _instance = null;
        public static Player instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<Player> (); return _instance; } }
        public override MaterialSwitcher matSwitch { get { if (_matSwitch == null) _matSwitch = GetComponentInChildren<MaterialSwitcher> (); return _matSwitch; } }
        private TrailRenderer _trail = null;
        public TrailRenderer trail { get { if (_trail == null) _trail = GetComponentInChildren<TrailRenderer> (); return _trail; } }

        [SerializeField] InputControlType CurrentInputType;
        public InputSettings inputSettings;
        [SerializeField] Dictionary<InputControlType, IInputController> InputController;
        [SerializeField] GameEvent OnDeath;
        public Material TunnelMatertial;
        public Material GateMatertial;
        public UnityEvent StartOnTrack;
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
        public void SetColor (Color color)
        {
            matSwitch.SetColorInMaterial (color);
            TunnelMatertial.DOColor (color, "_Color", 1); //_Emission
            GateMatertial.DOColor (color, "_EmissionColor", 1); //_Emission
            // matSwitch.SetColorInMaterial(color, "_Color");
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

            inputSettings.SwipeDuration = 0.2f;/* minTimeOfBit */;
            foreach (var item in InputController)
                item.Value.Init (transform, inputSettings);
        }

        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }
        public override void Death ()
        {
            base.Death ();
            OnDeath.Raise ();
            gameObject.SetActive (false);
        }

    }
}
