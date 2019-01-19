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
            cdButton.Init (boosterData.LifeTime, boosterData.Activate);
            boosterData.shopData.Count.OnValueChanged += InitCDButton;
        }
        private void OnDestroy ()
        {
            boosterData.shopData.Count.OnValueChanged -= InitCDButton;
        }
        private void InitCDButton (int count)
        {
            if (count != 0)
                cdButton.Init (boosterData.LifeTime, boosterData.Activate);
        }

        public void OnBoosterReseted (BoosterData boosterData)
        {
            if (boosterData.Equals (this.boosterData))
            {
                cdButton.Reset ();
                cdButton.Init (boosterData.LifeTime, boosterData.Activate);
            }
        }

    }
}
