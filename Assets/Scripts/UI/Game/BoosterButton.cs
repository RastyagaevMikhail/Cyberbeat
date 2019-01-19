using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
namespace CyberBeat
{

    [RequireComponent (typeof (CDButton))]
    public class BoosterButton : MonoBehaviour
    {
        private CDButton _cdButton = null;
        public CDButton cdButton { get { if (_cdButton == null) _cdButton = GetComponent<CDButton> (); return _cdButton; } }

        [SerializeField] BoosterData boosterData;

        private void Awake ()
        {
            boosterData.Count.OnValueChanged += InitCDButton;
            InitCDButton (boosterData.Count.Value);
        }
        private void OnDestroy ()
        {
            boosterData.Count.OnValueChanged -= InitCDButton;
        }
        private void InitCDButton (int count)
        {
            cdButton.Interractable = count != 0;

            cdButton.CDTime = boosterData.LifeTime;
        }

        public void OnBoosterReseted (BoosterData boosterData)
        {
            if (boosterData.Equals (this.boosterData))
            {
                cdButton.Reset ();
                cdButton.CDTime = boosterData.LifeTime;
            }
        }

    }
}
