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
        [SerializeField] MaterialSwitcher trail ;

        [SerializeField] InputControlType CurrentInputType;
        public InputSettings inputSettings;
        [SerializeField] Dictionary<InputControlType, IInputController> InputController;
        [SerializeField] GameEvent OnDeath;
        [SerializeField] GameEventColor OnColorChnaged;

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

        public void GEColor_OnColorChangeOnInterracor (Color color)
        {
            if (!ChechColor (color))
            {
                OnColorChnaged.Raise (color);
            }
        }
        public void SetColor (Color color)
        {
            matSwitch.SetColorInMaterial (color);

            if (trail)
                trail.SetColor ( color);
        }

        private void Awake ()
        {
            SetColor (Color.white);

            inputSettings.SwipeDuration = 0.2f;
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
