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

        private void Start ()
        {
            SetColor (Color.white);
        }
        public bool ChechColor (Color color)
        {
            return matSwitch.ChechColor (color);
        }

        public void OnContactWithColorInterractor (ColorInterractor.Info info)
        {
            if (info.isSwitcher)
            {
                SetColor (info.color);
                return;
            }

            if (ChechColor (info.color)) return;
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
