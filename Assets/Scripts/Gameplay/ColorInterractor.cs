using GameCore;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public abstract class ColorInterractor : Interractor
    {
        protected MaterialSwitcher _matSwitch = null;
        public MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponent<MaterialSwitcher> (); } return _matSwitch; } }

        public Color CurrentColor { get { return matSwitch.CurrentColor; } }

        float bit;
        [Serializable]
        public struct Info
        {
            public bool isSwitcher;
            public Color color;
        }

        [SerializeField] Info CurrentInfo;

        public void Init (float bitTime)
        {
            bit = bitTime;
        }

        [Header ("OnDeth Events")]
        [SerializeField] UnityEventColorInterractorInfo OnDeathASColorInterractorInfo;
        [SerializeField] UnityEventColor OnDeathAsColor;
        [SerializeField] UnityEventFloat OnDeathAsBit;
        [SerializeField] UnityEvent OnDeath;
        public virtual void Death ()
        {
            CurrentInfo.color = CurrentColor;
            OnDeathASColorInterractorInfo.Invoke (CurrentInfo);

            OnDeathAsBit.Invoke (bit);

            OnDeathAsColor.Invoke (CurrentColor);

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
