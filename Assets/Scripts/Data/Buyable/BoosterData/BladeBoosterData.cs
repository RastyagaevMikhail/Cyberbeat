using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Blade", menuName = "CyberBeat/BoosterData/Blade")]
    public class BladeBoosterData : BoosterData
    {
        public override bool Apply (ColorBrick brick, bool equalsColor)
        {
            OnApply.Invoke (this);

            brick.KillNeighbors ();
            
            return equalsColor;
        }
    }
}
