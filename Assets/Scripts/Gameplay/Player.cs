using DG.Tweening;

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

        [SerializeField] BoosterDataRuntimeSet activeBoosters;
        [Header ("Events")]
        [SerializeField] UnityEvent OnDeath;

        public void SetColor (Color color)
        {
            matSwitch.CurrentColor = color;
        }

        private void Awake ()
        {
            matSwitch.CurrentMaterial.DOColor (Color.white, 1f);
        }
        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }

        public void OnContactWithColorInterractor (ColorInterractor colorInterractor)
        {
            bool equalsColor = ChechColor (colorInterractor.CurrentColor);

            ColorBrick colorBrick = colorInterractor as ColorBrick;
            bool notDie = equalsColor;
            if (colorBrick)
            {
                activeBoosters
                    .ForEach (boosterData =>
                        notDie |= boosterData.Apply (colorBrick, equalsColor)
                    );
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
