using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Blade", menuName = "CyberBeat/BoosterData/Blade")]
    public class BladeBoosterData : BoosterData
    {
        public override void Apply (ColorBrick brick)
        {
            Debug.Log ("Apply.BladeBoosterData");
            foreach (var neighbor in brick.Neighbors)
                neighbor.Death ();
            brick.OnPlayerContact();
        }
    }
}
