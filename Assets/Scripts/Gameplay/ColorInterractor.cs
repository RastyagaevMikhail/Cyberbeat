using GameCore;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class ColorInterractor : Interractor
    {
        protected MaterialSwitcher _matSwitch = null;
        public MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponent<MaterialSwitcher> (); } return _matSwitch; } }
        public Color CurrentColor { get { return matSwitch.CurrentColor; } }

        float bit;
        string particlesKey;
        [SerializeField] bool isSwitcher;
        public bool IsSwitcher => isSwitcher;
        public void Init (float bitTime, string keyParticles)
        {
            bit = bitTime;
            particlesKey = keyParticles;

        }

        [Header ("OnDeth Events")]
        [SerializeField] UnityEventColorBool OnDeathWithColorIsSwitcher;
        [SerializeField] UnityEventColor OnDeathAsColor;
        [SerializeField] UnityEventFloat OnDeathAsBit;
        [SerializeField] UnityEventString OnDeathAsString;
        [SerializeField] UnityEvent OnDeath;
        public virtual void Death ()
        {
            OnDeathWithColorIsSwitcher.Invoke (CurrentColor, isSwitcher);

            OnDeathAsBit.Invoke (bit);

            OnDeathAsColor.Invoke (CurrentColor);
            OnDeathAsString.Invoke (particlesKey);

            OnDeath.Invoke ();

            // neighbors.Clear ();
        }

        // [SerializeField] // Test neighbors
        List<ColorInterractor> neighbors = new List<ColorInterractor> ();
        public void AddNeighbor (ColorInterractor OtherInterractor)
        {
            if (!this.Equals (OtherInterractor) &&
                !neighbors.Contains (OtherInterractor))
            {
                neighbors.Add (OtherInterractor);
            }
        }
        public void KillNeighbors ()
        {
            foreach (var neighbor in neighbors)
                neighbor.Death ();
        }
    }
}
