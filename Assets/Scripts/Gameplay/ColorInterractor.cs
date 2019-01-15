using GameCore;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public abstract class ColorInterractor : Interractor
    {
        protected MaterialSwitcher _matSwitch = null;
        public virtual MaterialSwitcher matSwitch { get { if (_matSwitch == null) { _matSwitch = GetComponent<MaterialSwitcher> (); } return _matSwitch; } }
        float bit;

        public void Init (float bitTime)
        {
            bit = bitTime;
        }

        [Header ("OnDeth Events")]
        [SerializeField] UnityEventColorInterractor OnDeathASColorInterractor;
        [SerializeField] UnityEventColor OnDeathAsColor;
        [SerializeField] UnityEventFloat OnDeathAsBit;
        [SerializeField] UnityEvent OnDeath;
        public virtual void Death ()
        {
            OnDeathASColorInterractor.Invoke (this);

            OnDeathAsBit.Invoke (bit);

            OnDeathAsColor.Invoke (matSwitch.CurrentColor);

            OnDeath.Invoke ();

            neighbors.Clear ();
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
