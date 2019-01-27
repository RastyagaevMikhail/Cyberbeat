﻿using DG.Tweening;

using GameCore;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class Player : MonoBehaviour
    {
        [SerializeField] MaterialSwitcherVariable matSwitch;

        [Header ("Events")]
        [SerializeField] UnityEvent OnDeath;

        public void SetColor (Color color)
        {
            matSwitch.CurrentColor = color;
        }

        private void Awake ()
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

            if (ChechColor(info.color)) return;
            Death ();
        }

        [ContextMenu ("Death")]
        private void Death ()
        {
            OnDeath.Invoke ();
        }
    }
}
