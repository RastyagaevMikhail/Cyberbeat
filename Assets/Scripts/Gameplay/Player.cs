using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class Player : MonoBehaviour
    {
        [SerializeField] MaterialSwitcherVariable matSwitch;

        [SerializeField] UnityEvent OnDeath;

        public void SetColor (Color color)
        {
            matSwitch.CurrentColor = color;
        }
        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }

        public void OnContactWithColorInterractor (Color color, bool isSwitcher)
        {
            if (isSwitcher)
            {
                SetColor (color);
                return;
            }

            if (ChechColor (color)) return;
            Death ();
        }

        [Button]
        [ContextMenu ("Death")]
        public void Death ()
        {
            OnDeath.Invoke ();
        }
    }
}
