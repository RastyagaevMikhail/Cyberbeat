using GameCore;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class Player : TransformObject
    {
        private MaterialSwitcher _matSwitch;
        public MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponentInChildren<MaterialSwitcher> (); } return _matSwitch; } }

        [SerializeField] TransformVariable myTransform;
        [SerializeField] BoosterDataRuntimeSet activeBoosters;
        [Header ("Events")]
        [SerializeField] UnityEvent OnDeath;

        public void SetColor (Color color)
        {
            matSwitch.SetColorInMaterial (color);
            // OnColorChnaged.Raise (color);
        }

        private void Awake ()
        {
            myTransform.Value = transform;

            SetColor (Color.white);
        }
        public void OnColorTeked (Color color)
        {

        }
        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }

        public void OnContactWithColorInterractor (ColorInterractor colorInterractor)
        {
            bool equalsColor = matSwitch.ChechColor (colorInterractor.matSwitch.CurrentColor);
            ColorBrick colorBrick = colorInterractor as ColorBrick;
            bool notDie = equalsColor;
            if (colorBrick)
            {
                activeBoosters.ForEach (boosterData =>
                    notDie |= boosterData.Apply (colorBrick, equalsColor)
                );
                // Debug.LogFormat ("activeBoosters.Count = {0}", activeBoosters.Count);
                // Debug.LogFormat ("notDie = {0}", notDie);
            }
            else
            {
                notDie = true;
            }
            colorInterractor.Death ();

            if (notDie) return;
            Death ();
        }

        [ContextMenu ("Death")]
        private void Death ()
        {
            OnDeath.Invoke ();
        }
    }
}
