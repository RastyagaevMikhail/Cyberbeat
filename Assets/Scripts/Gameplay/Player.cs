using GameCore;

using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class Player : Interractor
    {
        private static Player _instance = null;
        public static Player instance { get { if (_instance == null) { _instance = GameObject.FindObjectOfType<Player> (); } return _instance; } }
        private MaterialSwitcher _matSwitch;
        public MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponentInChildren<MaterialSwitcher> (); } return _matSwitch; } }

        [SerializeField] InputControlType CurrentInputType;
        public InputSettings inputSettings;
        [SerializeField] InputController inputController;
        [SerializeField] GameEvent OnDeath;
        [SerializeField] GameEventColor OnColorChnaged;

        public void SetControl (InputControlType controlTypeToSwitch)
        {
            CurrentInputType = controlTypeToSwitch;
        }
        public void MoveRight ()
        {
            inputController[CurrentInputType].MoveRight ();
        }
        public void MoveLeft ()
        {
            inputController[CurrentInputType].MoveLeft ();
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
        }

        private void Awake ()
        {
            OnColorChnaged.Raise (Color.white);
            SetColor (Color.white);

            inputSettings.SwipeDuration = 0.2f;
            foreach (var item in inputController.controllers)
            {
                item.controller.Init (transform, inputSettings);
            }
        }
        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }
        public void Death ()
        {
            Pool.instance.Pop ("deathPlayer", transform.parent);

            OnDeath.Raise ();
            gameObject.SetActive (false);
        }
    }
}
